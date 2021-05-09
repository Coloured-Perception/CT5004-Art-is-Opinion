// Optimized Mobile Painter - Unitycoder.com
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Tobii.Gaming;
using UnityEngine.SceneManagement;

namespace unitycoder_MobilePaint {
	public enum DrawMode {
		Default,
		CustomBrush,
		FloodFill,
		Pattern,
		ShapeLines,
		Eraser
	}

	public enum EraserMode {
		Default,
		BackgroundColor
	}

	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class MobilePaint : MonoBehaviour {
		// Declaring variables related to Tobii eye-tracker input
		[Header("Tobii Eye-tracking")]
		public bool isEyeTracker = true;    // Whether the player is using Tobii eye-tracker, Turn false to test without eye-tracker
		bool isGazing = true;               // Used to say whether the player is looking at a point long enough
		List<GazePoint> gazePoint;          // A list of co-ordinates where the player is looking
		Vector2 filteredPoint;

		[Header("Mouse or Touch")]
		public bool enableTouch = false;

		[Space(10)]
		public LayerMask paintLayerMask = 1 << 0;   // to which layer our paint canvas is at (used in raycast)

		public bool createCanvasMesh = false;   // default canvas is full screen quad, if disabled existing mesh is used

		public RectTransform referenceArea;     // we will match the size of this reference object
		private float canvasScaleFactor = 1;    // canvas scaling factor (will be taken from Canvas)

		[Header("Brush Settings")]
		public bool connectBrushStokes = true;  // if brush moves too fast, then connect them with line. NOTE! Disable this if you are painting to custom mesh

		// Default settings for brush
		public Color32 paintColor = new Color32(255, 32, 32, 32);   // Colour: Black
		public int brushSize = 24;
		public int brushSizeMin = 10;
		public int brushSizeMax = 64;

		// cached calculations
		public bool hiQualityBrush = false;     // Draw more brush strokes when moving NOTE: this is slow on mobiles!
		private int brushSizeX1 = 48;           // << 1
		private int brushSizeXbrushSize = 576;  // x*x
		private int brushSizeX4 = 96;           // << 2
		private int brushSizeDiv4 = 6;          // >> 2 == /4

		public bool realTimeTexUpdate = true;   // if set to true, ignore textureUpdateSpeed, and always update when textureNeedsUpdate gets set to true when drawing
		public float textureUpdateSpeed = 0.1f; // how often texture should be updated (0 = no delay, 1 = every one seconds)
		private float nextTextureUpdate = 0;

		public bool useAdditiveColors = false;  // true = alpha adds up slowly, false = 1 click will instantly set alpha to brush or paint color alpha value

		public float brushAlphaStrength = 0.01f;        // multiplier to soften brush additive alpha, 0.1f is nice & smooth, 1 = faster
		private float brushAlphaStrengthVal = 0.01f;    // cached calculation
		private float alphaLerpVal = 0.1f;
		private float brushAlphaLerpVal = 0.1f;

		[Header("Options")]
		public DrawMode drawMode = DrawMode.Default;    // drawing modes: 0 = Default, 1 = custom brush, 2 = floodfill
		public bool useLockArea = false;                // locking mask: only paint in area of the color that your click first
		public bool useMaskLayerOnly = false;           // if true, only check pixels from mask layer, not from the painted texture
		public bool smoothenMaskEdges = false;          // less white edges with mask
		public bool useThreshold = false;
		public byte paintThreshold = 128;               // 0 = only exact match, 255 = match anything

		// ERASER
		[Space(10)]
		public EraserMode eraserMode = EraserMode.BackgroundColor;

		// AREA FILL CALCULATIONS
		[Space(10)]
		public bool getAreaSize = false;    // NOTE: to use this, someone has to listen the event AreaWasPainted (see scene "scene_MobilePaint_LockingMaskWithAreaCalculation")
		int initialX = 0;
		int initialY = 0;
		public delegate void AreaWasPainted(int fullArea, int filledArea, float percentageFilled, Vector3 point);
		public event AreaWasPainted AreaPaintedEvent;

		private byte[] lockMaskPixels;  // locking mask pixels

		public bool canDrawOnBlack = true;  // to stop filling on mask black lines, FIXME: not working if its not pure black...

		public Vector2 canvasSizeAdjust = new Vector2(0, 0);    // this means, "ScreenResolution.xy+screenSizeAdjust.xy" (use only minus values, to add un-drawable border on right or bottom)
		public string targetTexture = "_MainTex";               // target texture for this material shader (usually _MainTex)
		public FilterMode filterMode = FilterMode.Point;

		// canvas clear color
		public Color32 clearColor = new Color32(255, 255, 255, 255);

		[Header("Mask/Overlay")]
		public bool useMaskImage = false;
		public Texture2D maskTex;

		[Header("Custom Brushes")]
		public bool useCustomBrushes = false;
		public Texture2D[] customBrushes;
		public bool overrideCustomBrushColor = false;   // uses paint color instead of brush texture color
		public bool useCustomBrushAlpha = true;         // true = use alpha from brush, false = use alpha from current paint color
		public int selectedBrush = 0;                   // currently selected brush index

		private byte[] customBrushBytes;
		private int customBrushWidth;
		private int customBrushHeight;
		private int customBrushWidthHalf;
		private int texWidthMinusCustomBrushWidth;
		private int texHeightMinusCustomBrushHeight;

		[Header("Custom Patterns")]
		public bool useCustomPatterns = false;
		public Texture2D[] customPatterns;
		private byte[] patternBrushBytes;
		private int customPatternWidth;
		private int customPatternHeight;
		public int selectedPattern = 0;

		[Header("Line Drawing")]
		public Transform previewLineCircle;
		Transform previewLineCircleStart;       // clone for start of circle
		Transform previewLineCircleEnd;         // clone for end of circle
		bool haveStartedLine = false;
		int firstClickX = 0;
		int firstClickY = 0;
		LineRenderer lineRenderer;
		public bool snapLinesToGrid = false;    // while drawing lines
		public int gridResolution = 128;
		int gridSize = 10;

		// for old GUIScaling
		private float scaleAdjust = 1.0f;
		private const float BASE_WIDTH = 800;
		private const float BASE_HEIGHT = 480;

		//	*** private variables, no need to touch ***
		private byte[] pixels;      // byte array for texture painting, this is the image that we paint into.
		private byte[] maskPixels;  // byte array for mask texture
		private byte[] clearPixels; // byte array for clearing texture

		private Texture2D drawingTexture;   // texture that we paint into (it gets updated from pixels[] array when painted)

		[Header("Overrides")]
		public float resolutionScaler = 1.0f;   // 1 means screen resolution, 0.5f means half the screen resolution
		public bool overrideResolution = false;
		public int overrideWidth = 1024;
		public int overrideHeight = 768;

		private int texWidth;
		private int texHeight;
		private Touch touch;                // touch reference
		private bool wasTouching = false;   // in previous frame we had touch
		private Camera cam;                 // main camera reference
		private Renderer myRenderer;

		private RaycastHit hit;
		private bool wentOutside = false;

		private bool usingClearingImage = false;    // did we have initial texture as maintexture, then use it as clear pixels array

		private Vector2 pixelUV;    // with mouse
		private Vector2 pixelUVOld; // with mouse

		private Vector2[] pixelUVs;     // mobiles
		private Vector2[] pixelUVOlds;  // mobiles

		[HideInInspector]
		public bool textureNeedsUpdate = false; // if we have modified texture

		[Header("Misc")]
		public bool undoEnabled = false;
		private List<byte[]> undoPixels;    // undo buffer(s)
		private int maxUndoBuffers = 10;    // how many undo buffers are kept in memory
		public GameObject userInterface;
		public bool hideUIWhilePainting = false;
		private bool isUIVisible = true;

		// Debug mode, outputs debug info when used
		public bool debugMode = false;

		// for checking if UI element is clicked, then dont paint under it
		EventSystem eventSystem;

		// zoom pan
		private bool isZoomingOrPanning = false;

		public PaintDetailsScript paintDetails;

		void Awake() {
			// cache components
			cam = Camera.main;
			myRenderer = GetComponent<Renderer>();

			if (gameObject.GetComponent<MeshCollider>() == null) { gameObject.AddComponent<MeshCollider>(); }

			GameObject go = GameObject.Find("EventSystem");
			if (go == null) {
				Debug.LogError("GameObject EventSystem is missing from scene, will have problems with the UI", gameObject);
			} else {
				eventSystem = go.GetComponent<EventSystem>();
			}

			StartupValidation();
			InitializeEverything();

			gazePoint = new List<GazePoint>();
		}

		/// <summary>
		/// All startup validations will be moved here
		/// </summary>
		void StartupValidation() {
			if (cam == null) { Debug.LogError("MainCamera not founded, you must have 1 camera active & tagged as MainCamera", gameObject); }

			if (userInterface == null) {
				if (hideUIWhilePainting) {
					Debug.LogWarning("UI Canvas / userInterface not assigned - disabling hideUIWhilePainting", gameObject);
					hideUIWhilePainting = false;
				}
			}

			// Custom brushes validation
			if (useCustomBrushes && (customBrushes == null || customBrushes.Length < 1)) {
				Debug.LogWarning("useCustomBrushes is enabled, but no custombrushes assigned to array, disabling customBrushes", gameObject);
				useCustomBrushes = false;
			}

			// Custom patterns validation
			if (useCustomPatterns && (customPatterns == null || customPatterns.Length < 1)) {
				Debug.LogWarning("useCustomPatterns is enabled, but no customPatterns assigned to array, disabling useCustomPatterns", gameObject);
				useCustomPatterns = false;
			}

			// MASK validation
			if (useMaskImage) {
				if (maskTex == null) {
					Debug.LogWarning("maskImage is not assigned. Setting 'useMaskImage' to false", gameObject);
					useMaskImage = false;
					if (overrideResolution) { Debug.LogWarning("overrideResolution cannot be used, when useMaskImage is true", gameObject); }
				}
			}

			// check if target texture exists
			if (!myRenderer.material.HasProperty(targetTexture)) { Debug.LogError("Fatal error: Current shader doesn't have a property: '" + targetTexture + "'", gameObject); }

			if (getAreaSize) {
				if (!useThreshold || !useMaskLayerOnly) {
					Debug.LogWarning("getAreaSize is enabled, but both useThreshold or useMaskLayerOnly are not enabled, getAreaSize might not work", gameObject);
				}
			}

			if (paintLayerMask.value == 0) { Debug.LogWarning("paintLayerMask is set to 'nothing', assign same layer where the drawing canvas is", gameObject); }

			// validate & clamp override resolution
			if (overrideResolution) {
				if (overrideWidth < 0 || overrideWidth > 8192) { Debug.LogWarning("Invalid overrideWidth:" + overrideWidth, gameObject); overrideWidth = Mathf.Clamp(0, 1, 8192); }
				if (overrideHeight < 0 || overrideHeight > 8192) { Debug.LogWarning("Invalid overrideHeight:" + overrideHeight, gameObject); overrideHeight = Mathf.Clamp(0, 1, 8192); }

				if (resolutionScaler != 1) {
					Debug.LogWarning("Cannot use resolutionScaler with OverrideResolution, setting resolutionScaler to default (1)");
					resolutionScaler = 1;
				}
			}

			// check eraser modes
			if (myRenderer.material.GetTexture(targetTexture) == null) {
				if (eraserMode == EraserMode.Default) {
					Debug.LogError("eraserMode is set to Default, but there is no texture assigned to " + targetTexture + ". Setting eraseMode to BackgroundColor");
					eraserMode = EraserMode.BackgroundColor;
				}
			}
		}   // StartupValidation()

		/// <summary>
		/// Rebuilds everything and reloads masks,textures...
		/// </summary>
		public void InitializeEverything() {
			// for drawing lines preview
			if (GetComponent<LineRenderer>() != null) {
				lineRenderer = GetComponent<LineRenderer>();

				// reset pos
				lineRenderer.SetPosition(0, Vector3.one * 99999);
				lineRenderer.SetPosition(1, Vector3.one * 99999);

				if (previewLineCircle) {
					// spawn rounded circles for linedrawing, if not already in scene
					if (!previewLineCircleStart) { previewLineCircleStart = Instantiate(previewLineCircle) as Transform; }
					if (!previewLineCircleEnd) { previewLineCircleEnd = Instantiate(previewLineCircle) as Transform; }

					// hide them far away
					previewLineCircleStart.position = Vector3.one * 99999;
					previewLineCircleEnd.position = Vector3.one * 99999;
				}

				UpdateLineModePreviewObjects();
			}

			// cached calculations
			brushSizeX1 = brushSize << 1;
			brushSizeXbrushSize = brushSize * brushSize;
			brushSizeX4 = brushSizeXbrushSize << 2;

			SetBrushAlphaStrength(brushAlphaStrength);
			SetPaintColor(paintColor);

			// calculate scaling ratio for different screen resolutions
			float _baseHeightInverted = 1.0f / BASE_HEIGHT;
			float ratio = (Screen.height * _baseHeightInverted) * scaleAdjust;
			canvasSizeAdjust *= ratio;

			// WARNING: fixed maximum amount of touches, is set to 20 here. Not sure if some device supports more?
			pixelUVs = new Vector2[20];
			pixelUVOlds = new Vector2[20];

			// create texture
			if (useMaskImage) {
				SetMaskImage(maskTex);
			} else {    // no mask texture
						// overriding will also ignore Resolution Scaler value
				if (overrideResolution) {
					var err = false;
					if (overrideWidth < 0 || overrideWidth > 4096) { err = true; }
					if (overrideHeight < 0 || overrideHeight > 4096) { err = true; }
					if (err) { Debug.LogError("overrideWidth or overrideWidth is invalid - clamping to 4 or 4096"); }
					texWidth = (int)Mathf.Clamp(overrideWidth, 4, 4096);
					texHeight = (int)Mathf.Clamp(overrideHeight, 4, 4096);
				} else {    // use screen size as texture size
					texWidth = (int)(Screen.width * resolutionScaler + canvasSizeAdjust.x);
					texHeight = (int)(Screen.height * resolutionScaler + canvasSizeAdjust.y);
				}
			}

			// we have no texture set for canvas, FIXME: this returns true if called initialize again, since texture gets created after this
			if (myRenderer.material.GetTexture(targetTexture) == null && !usingClearingImage)   // temporary fix by adding && !usingClearingImage
			{
				// create new texture
				if (drawingTexture != null) {
					Texture2D.DestroyImmediate(drawingTexture, true);   // cleanup old texture
				}
				drawingTexture = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);
				myRenderer.material.SetTexture(targetTexture, drawingTexture);

				// init pixels array
				pixels = new byte[texWidth * texHeight * 4];
			} else {    // we have canvas texture, then use that as clearing texture
				usingClearingImage = true;

				if (overrideResolution) { Debug.LogWarning("overrideResolution is not used, when canvas texture is assiged to material, we need to use the texture size"); }

				texWidth = myRenderer.material.GetTexture(targetTexture).width;
				texHeight = myRenderer.material.GetTexture(targetTexture).height;

				// init pixels array
				pixels = new byte[texWidth * texHeight * 4];

				if (drawingTexture != null) {
					Texture2D.DestroyImmediate(drawingTexture, true);   // cleanup old texture
				}
				drawingTexture = new Texture2D(texWidth, texHeight, TextureFormat.RGBA32, false);

				// we keep current maintex and read it as "clear pixels array" (so when "clear image" is clicked, original texture is restored
				ReadClearingImage();

				myRenderer.material.SetTexture(targetTexture, drawingTexture);
			}

			// set texture modes
			drawingTexture.filterMode = filterMode;
			drawingTexture.wrapMode = TextureWrapMode.Clamp;

			// locking mask enabled
			if (useLockArea) {
				lockMaskPixels = new byte[texWidth * texHeight * 4];
			}

			// grid for line shapes
			gridSize = texWidth / gridResolution;

			// init custom brush if used
			if (useCustomBrushes && drawMode == DrawMode.CustomBrush) { ReadCurrentCustomBrush(); }

			// whats our final resolution
			if (debugMode) { Debug.Log("Texture resolution:" + texWidth + "x" + texHeight); }

			// init undo buffer
			if (undoEnabled) { undoPixels = new List<byte[]>(); }

			ClearImage(updateUndoBuffer: false);
		}   // InitializeEverything

		/// <summary>
		/// Function used to allow the player to draw using the Tobii eye-tracker.
		/// </summary>
		void EyeTracker() {
			Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;  // Fetches the current co-ordinates on the screen that the player is looking at via the eye-tracker
			filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);

			if (isGazing) {
				// TEST: Undo key for desktop

				// mouse is over UI element? then dont paint
				if (eventSystem.IsPointerOverGameObject()) { return; }
				if (eventSystem.currentSelectedGameObject != null) { return; }

				// catch first mousedown
				//if (Input.GetKeyDown("space")) {
				//	if (hideUIWhilePainting && isUIVisible) HideUI();

				//	// when starting, grab undo buffer first
				//	if (undoEnabled) GrabUndoBufferNow();

				//	// if lock area is used, we need to take full area before painting starts
				//	if (useLockArea) {
				//		if (!Physics.Raycast(cam.ScreenPointToRay(filteredPoint), out hit, Mathf.Infinity, paintLayerMask)) return;

				//		CreateAreaLockMask((int)(hit.textureCoord.x * texWidth), (int)(hit.textureCoord.y * texHeight));
				//	}
				//}

				// left button is held down, draw
				if (Input.GetKey("space")) {
					// Only if we hit something, then we continue
					if (!Physics.Raycast(cam.ScreenPointToRay(filteredPoint), out hit, Mathf.Infinity, paintLayerMask)) { wentOutside = true; return; }

					pixelUVOld = pixelUV;   // take previous value, so can compare them
					pixelUV = hit.textureCoord;
					pixelUV.x *= texWidth;
					pixelUV.y *= texHeight;

					if (wentOutside) { pixelUVOld = pixelUV; wentOutside = false; }

					// lets paint where we hit
					switch (drawMode) {
						case DrawMode.Default:  // brush
							break;

						case DrawMode.Pattern:
							break;

						case DrawMode.CustomBrush:
							DrawCustomBrush((int)pixelUV.x, (int)pixelUV.y);
							break;

						case DrawMode.FloodFill:
							if (pixelUVOld == pixelUV) { break; }
							break;

						case DrawMode.ShapeLines:
							if (snapLinesToGrid) {
							} else {
							}
							break;

						case DrawMode.Eraser:
							if (eraserMode == EraserMode.Default) {
							} else {
							}
							break;

						default:    // unknown DrawMode
							Debug.LogError("Unknown drawMode");
							break;
					}

					if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
						if (!paintDetails.coloursUsed.Contains(paintColor)) {
							paintDetails.coloursUsed.Add(paintColor);
							//Debug.Log(paintDetails.coloursUsed.Count);
						}
						if (!paintDetails.brushesUsed.Contains(selectedBrush)) {
							paintDetails.brushesUsed.Add(selectedBrush);
							//Debug.Log(paintDetails.brushesUsed.Count);
						}
					}

					textureNeedsUpdate = true;
				}

				if (Input.GetKeyDown("space")) {
					// take this position as start position
					if (!Physics.Raycast(cam.ScreenPointToRay(filteredPoint), out hit, Mathf.Infinity, paintLayerMask)) { return; }

					pixelUVOld = pixelUV;
				}

				// check distance from previous drawing point and connect them with DrawLine
				if (connectBrushStokes && textureNeedsUpdate) {
					switch (drawMode) {
						case DrawMode.Default:  // drawing
							break;

						case DrawMode.CustomBrush:
							DrawLineWithBrush(pixelUVOld, pixelUV);
							break;

						case DrawMode.Pattern:
							break;

						case DrawMode.Eraser:
							if (eraserMode == EraserMode.Default) {
							} else {
							}
							break;

						default:    // other modes
							break;
					}
					pixelUVOld = pixelUV;
					textureNeedsUpdate = true;
				}

				// left mouse button released
				if (Input.GetKeyDown("space")) {
					// calculate area size
					if (getAreaSize && useLockArea && useMaskLayerOnly && drawMode != DrawMode.FloodFill) {
					}

					// end shape line here
					if (drawMode == DrawMode.ShapeLines) {
						haveStartedLine = false;

						// hide preview line
						lineRenderer.SetPosition(0, Vector3.one * 99999);
						lineRenderer.SetPosition(1, Vector3.one * 99999);
						previewLineCircleStart.position = Vector3.one * 99999;
						previewLineCircleEnd.position = Vector3.one * 99999;

						// draw actual line from start to current pos
						if (snapLinesToGrid) {
							Vector2 extendLine = (pixelUV - new Vector2((float)firstClickX, (float)firstClickY)).normalized * (brushSize * 0.25f);
						} else {
							// need to extend line to avoid too short start/end
							Vector2 extendLine = (pixelUV - new Vector2((float)firstClickX, (float)firstClickY)).normalized * (brushSize * 0.25f);
						}
						textureNeedsUpdate = true;
					}
				}
			}
		}

		// *** MAINLOOP ***
		void Update() {
			// Checks to see what type of input the player is using
			if (isEyeTracker)   // Calls the function for Tobii eye-tracker input
			{
				EyeTracker();
			} else {    // Calls the function for Mouse input
				MousePaint();
			}

			if (textureNeedsUpdate && (realTimeTexUpdate || Time.time > nextTextureUpdate)) {
				nextTextureUpdate = Time.time + textureUpdateSpeed;
				UpdateTexture();
			}
		}

		/// <summary>
		/// Handle mouse events
		/// </summary>
		void MousePaint() {
			// left button is held down, draw
			if (Input.GetKey("space")) {
				// Only if we hit something, then we continue
				if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, paintLayerMask)) {
					wentOutside = true;
					return;
				}

				pixelUVOld = pixelUV;   // take previous value, so can compare them
				pixelUV = hit.textureCoord;
				pixelUV.x *= texWidth;
				pixelUV.y *= texHeight;

				if (wentOutside) { pixelUVOld = pixelUV; wentOutside = false; }

				// lets paint where we hit
				switch (drawMode) {
					case DrawMode.Default:  // brush
						break;

					case DrawMode.Pattern:
						break;

					case DrawMode.CustomBrush:
						DrawCustomBrush((int)pixelUV.x, (int)pixelUV.y);
						break;

					case DrawMode.FloodFill:
						if (pixelUVOld == pixelUV) { break; }
						break;

					case DrawMode.ShapeLines:
						if (snapLinesToGrid) {
						} else {
						}
						break;

					case DrawMode.Eraser:
						if (eraserMode == EraserMode.Default) {
						} else {
						}
						break;

					default:    // unknown DrawMode
						Debug.LogError("Unknown drawMode");
						break;
				}

				if (SceneManager.GetActiveScene().name == "PortraitPaintScene") {
					if (!paintDetails.coloursUsed.Contains(paintColor)) {
						paintDetails.coloursUsed.Add(paintColor);
						Debug.Log(paintDetails.coloursUsed.Count);
					}
					if (!paintDetails.brushesUsed.Contains(selectedBrush)) {
						paintDetails.brushesUsed.Add(selectedBrush);
						Debug.Log(paintDetails.brushesUsed.Count);
					}
				}

				textureNeedsUpdate = true;
			}

			if (Input.GetKeyDown("space")) {
				// take this position as start position
				if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, paintLayerMask)) { return; }

				pixelUVOld = pixelUV;
			}

			// check distance from previous drawing point and connect them with DrawLine
			if (connectBrushStokes && textureNeedsUpdate) {
				switch (drawMode) {
					case DrawMode.Default:  // drawing
						break;

					case DrawMode.CustomBrush:
						DrawLineWithBrush(pixelUVOld, pixelUV);
						break;

					case DrawMode.Pattern:
						break;

					case DrawMode.Eraser:
						if (eraserMode == EraserMode.Default) {
						} else {
						}
						break;

					default:    // other modes
						break;
				}
				pixelUVOld = pixelUV;
				textureNeedsUpdate = true;
			}

			// left mouse button released
			if (Input.GetKeyDown("space")) {
				// calculate area size
				if (getAreaSize && useLockArea && useMaskLayerOnly && drawMode != DrawMode.FloodFill) {
				}

				// end shape line here
				if (drawMode == DrawMode.ShapeLines) {
					haveStartedLine = false;

					// hide preview line
					lineRenderer.SetPosition(0, Vector3.one * 99999);
					lineRenderer.SetPosition(1, Vector3.one * 99999);
					previewLineCircleStart.position = Vector3.one * 99999;
					previewLineCircleEnd.position = Vector3.one * 99999;

					// draw actual line from start to current pos
					if (snapLinesToGrid) {
						Vector2 extendLine = (pixelUV - new Vector2((float)firstClickX, (float)firstClickY)).normalized * (brushSize * 0.25f);
					} else {
						// need to extend line to avoid too short start/end
						Vector2 extendLine = (pixelUV - new Vector2((float)firstClickX, (float)firstClickY)).normalized * (brushSize * 0.25f);
					}
					textureNeedsUpdate = true;
				}
			}
		}

		void UpdateTexture() {
			textureNeedsUpdate = false;
			drawingTexture.LoadRawTextureData(pixels);
			drawingTexture.Apply(false);
		}

		void CreateAreaLockMask(int x, int y) {
			initialX = x;
			initialY = y;

			if (useThreshold) {
				if (useMaskLayerOnly) {
					if (getAreaSize) {
					} else {
					}
				} else {
				}
			} else {    // no threshold
				if (useMaskLayerOnly) {
				} else {
				}
			}
		}

		/// <summary>
		/// Actual custom brush painting function
		/// </summary>
		/// <param name="px">Current x-position</param>
		/// <param name="py">Current y-position</param>
		void DrawCustomBrush(int px, int py) {
			// get position where we paint
			int startX = (int)(px - customBrushWidthHalf);
			int startY = (int)(py - customBrushWidthHalf);

			if (startX < 0) {
				startX = 0;
			} else {
				if (startX + customBrushWidth >= texWidth) { startX = texWidthMinusCustomBrushWidth; }
			}

			if (startY < 1) // TODO: temporary fix, 1 instead of 0
			{
				startY = 1;
			} else {
				if (startY + customBrushHeight >= texHeight) { startY = texHeightMinusCustomBrushHeight; }
			}

			int pixel = (texWidth * startY + startX) << 2;
			int brushPixel = 0;

			for (int y = 0; y < customBrushHeight; y++) {
				for (int x = 0; x < customBrushWidth; x++) {
					brushPixel = (customBrushWidth * (y) + x) << 2;

					// we have some color at this brush pixel?
					if (customBrushBytes[brushPixel + 3] > 0 && (!useLockArea || (useLockArea && lockMaskPixels[pixel] == 1))) {
						if (useCustomBrushAlpha)    // use alpha from brush
						{
							if (useAdditiveColors) {
								brushAlphaLerpVal = customBrushBytes[brushPixel + 3] * brushAlphaStrength * 0.01f;  // 0.01 is temporary fix so that default brush & custom brush both work

								if (overrideCustomBrushColor) {
									pixels[pixel] = ByteLerp(pixels[pixel], paintColor.r, brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], paintColor.g, brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], paintColor.b, brushAlphaLerpVal);
								} else {    // use paint color instead of brush texture
									pixels[pixel] = ByteLerp(pixels[pixel], customBrushBytes[brushPixel], brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], customBrushBytes[brushPixel + 1], brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], customBrushBytes[brushPixel + 2], brushAlphaLerpVal);
								}
								pixels[pixel + 3] = ByteLerp(pixels[pixel + 3], paintColor.a, brushAlphaLerpVal);
							} else {    // no additive colors
								if (overrideCustomBrushColor) {
									pixels[pixel] = ByteLerp(pixels[pixel], paintColor.r, brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], paintColor.g, brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], paintColor.b, brushAlphaLerpVal);
								} else {    // use paint color instead of brush texture
									pixels[pixel] = customBrushBytes[brushPixel];
									pixels[pixel + 1] = customBrushBytes[brushPixel + 1];
									pixels[pixel + 2] = customBrushBytes[brushPixel + 2];
								}
								pixels[pixel + 3] = customBrushBytes[brushPixel + 3];
							}
						} else {    // use paint color alpha
							if (useAdditiveColors) {
								if (overrideCustomBrushColor) {
									pixels[pixel] = ByteLerp(pixels[pixel], paintColor.r, brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], paintColor.g, brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], paintColor.b, brushAlphaLerpVal);
								} else {
									pixels[pixel] = ByteLerp(pixels[pixel], customBrushBytes[brushPixel], brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], customBrushBytes[brushPixel + 1], brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], customBrushBytes[brushPixel + 2], brushAlphaLerpVal);
								}
								pixels[pixel + 3] = ByteLerp(pixels[pixel + 3], paintColor.a, brushAlphaLerpVal);
							} else {    // no additive colors
								if (overrideCustomBrushColor) {
									pixels[pixel] = ByteLerp(pixels[pixel], paintColor.r, brushAlphaLerpVal);
									pixels[pixel + 1] = ByteLerp(pixels[pixel + 1], paintColor.g, brushAlphaLerpVal);
									pixels[pixel + 2] = ByteLerp(pixels[pixel + 2], paintColor.b, brushAlphaLerpVal);
								} else {
									pixels[pixel] = customBrushBytes[brushPixel];
									pixels[pixel + 1] = customBrushBytes[brushPixel + 1];
									pixels[pixel + 2] = customBrushBytes[brushPixel + 2];
								}
								pixels[pixel + 3] = customBrushBytes[brushPixel + 3];
							}
						}
					}
					pixel += 4;
				}   // for x

				pixel = (texWidth * (startY == 0 ? 1 : startY + y) + startX + 1) * 4;
			}   // for y
		}   // DrawCustomBrush

		/// <summary>
		/// Compares if two values are below threshold
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		bool CompareThreshold(byte a, byte b) {
			if (a < b) { a ^= b; b ^= a; a ^= b; } // http://lab.polygonal.de/?p=81
			return (a - b) <= paintThreshold;
		}

		/// <summary>
		/// Checks if two colours are the same
		/// </summary>
		/// <param name="a"></param>
		/// <param name="r">Red</param>
		/// <param name="g">Green</param>
		/// <param name="b">Blue</param>
		/// <returns>true = colours are the same, false = colours are different</returns>
		bool IsSameColor(Color32 a, byte r, byte g, byte b) {
			return (a.r == r && a.g == g && a.b == b);
		}

		/// <summary>
		/// Get custom brush texture into custombrushpixels array, this needs to be called if custom brush is changed
		/// </summary>
		public void ReadCurrentCustomBrush() {
			// NOTE: this works only for square brushes
			customBrushWidth = customBrushes[selectedBrush].width;
			customBrushHeight = customBrushes[selectedBrush].height;
			customBrushBytes = new byte[customBrushWidth * customBrushHeight * 4];

			int pixel = 0;
			Color32[] brushPixel = customBrushes[selectedBrush].GetPixels32();
			for (int y = 0; y < customBrushHeight; y++) {
				for (int x = 0; x < customBrushWidth; x++) {
					customBrushBytes[pixel] = brushPixel[x + y * customBrushWidth].r;
					customBrushBytes[pixel + 1] = brushPixel[x + y * customBrushWidth].g;
					customBrushBytes[pixel + 2] = brushPixel[x + y * customBrushWidth].b;
					customBrushBytes[pixel + 3] = brushPixel[x + y * customBrushWidth].a;
					pixel += 4;
				}
			}

			// precalculate few brush size values
			customBrushWidthHalf = (int)(customBrushWidth * 0.5f);
			texWidthMinusCustomBrushWidth = texWidth - customBrushWidth;
			texHeightMinusCustomBrushHeight = texHeight - customBrushHeight;
		}

		/// <summary>
		/// Draws single point to this pixel coordinate, with current paint color
		/// </summary>
		/// <param name="x">x-position</param>
		/// <param name="y">y-position</param>
		public void DrawPoint(int x, int y) {
			int pixel = (texWidth * y + x) * 4;
			pixels[pixel] = paintColor.r;
			pixels[pixel + 1] = paintColor.g;
			pixels[pixel + 2] = paintColor.b;
			pixels[pixel + 3] = paintColor.a;
		}

		/// <summary>
		/// Draws single point to this pixel array index, with current paint color
		/// </summary>
		/// <param name="pixel"></param>
		public void DrawPoint(int pixel) {
			pixels[pixel] = paintColor.r;
			pixels[pixel + 1] = paintColor.g;
			pixels[pixel + 2] = paintColor.b;
			pixels[pixel + 3] = paintColor.a;
		}

		/// <summary>
		/// ///////////////////////////////////////////////////////////////////needed otherwise the lines are patchy
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		void DrawLineWithBrush(Vector2 start, Vector2 end) {
			int x0 = (int)start.x;
			int y0 = (int)start.y;
			int x1 = (int)end.x;
			int y1 = (int)end.y;
			int tempVal = x1 - x0;
			int dx = (tempVal + (tempVal >> 31)) ^ (tempVal >> 31);	// http://stackoverflow.com/questions/6114099/fast-integer-abs-function
			tempVal = y1 - y0;
			int dy = (tempVal + (tempVal >> 31)) ^ (tempVal >> 31);
			int sx = x0 < x1 ? 1 : -1;
			int sy = y0 < y1 ? 1 : -1;
			int err = dx - dy;
			int pixelCount = 0;
			int e2;
			for (; ; )
			{
				if (hiQualityBrush) {
					DrawCustomBrush(x0, y0);
				} else {
					pixelCount++;
					if (pixelCount > brushSizeDiv4) {
						pixelCount = 0;

						DrawCustomBrush(x0, y0);
					}
				}
				if (x0 == x1 && y0 == y1) { break; }
				e2 = 2 * err;
				if (e2 > -dy) {
					err = err - dy;
					x0 = x0 + sx;
				} else if (e2 < dx) {
					err = err + dx;
					y0 = y0 + sy;
				}
			}
		}

		/// <summary>
		/// If this is called, undo buffer gets updated
		/// </summary>
		public void ClearImage() {
			ClearImage(true);
		}

		/// <summary>
		/// This override can be called with bool, to disable undo buffer grab
		/// </summary>
		/// <param name="updateUndoBuffer"></param>
		public void ClearImage(bool updateUndoBuffer) {
			if (undoEnabled && updateUndoBuffer) {
			}

			if (usingClearingImage) {
				ClearImageWithImage();
			} else {
				int pixel = 0;
				for (int y = 0; y < texHeight; y++) {
					for (int x = 0; x < texWidth; x++) {
						pixels[pixel] = clearColor.r;
						pixels[pixel + 1] = clearColor.g;
						pixels[pixel + 2] = clearColor.b;
						pixels[pixel + 3] = clearColor.a;
						pixel += 4;
					}
				}

				UpdateTexture();
			}
		}	// clear image

		public void ClearImageWithImage() {
			// fill pixels array with clearpixels array
			System.Array.Copy(clearPixels, 0, pixels, 0, clearPixels.Length);

			// just assign our clear image array into tex
			drawingTexture.LoadRawTextureData(clearPixels);
			drawingTexture.Apply(false);
		}	// clear image

		public void ReadMaskImage() {
			maskPixels = new byte[texWidth * texHeight * 4];

			int smoothenResolution = 5;	// currently fixed value
			int smoothArea = smoothenResolution * smoothenResolution;
			int smoothCenter = Mathf.FloorToInt(smoothenResolution / 2);

			int pixel = 0;
			Color c;

			for (int y = 0; y < texHeight; y++) {
				for (int x = 0; x < texWidth; x++) {
					if (smoothenMaskEdges) {
						c = new Color(0, 0, 0, 0);
						c = maskTex.GetPixel(x, y);	// center

						if (c.a > 0) {
							for (int i = 0; i < smoothArea; i++) {
								int xx = (i / smoothenResolution) | 0;	// 0, 0, 0
								int yy = i % smoothenResolution;
								if (maskTex.GetPixel(x + xx - smoothCenter, y + yy - smoothCenter).a < (255 - paintThreshold) / 255f) {
									c = new Color(0, 0, 0, 0);
								}
							}
						}
					} else {	// default (works well if texture is "point" filter mode
						c = maskTex.GetPixel(x, y);
					}
					maskPixels[pixel] = (byte)(c.r * 255);
					maskPixels[pixel + 1] = (byte)(c.g * 255);
					maskPixels[pixel + 2] = (byte)(c.b * 255);
					maskPixels[pixel + 3] = (byte)(c.a * 255);
					pixel += 4;
				}
			}
		}

		/// <summary>
		/// Reads original drawing canvas texture, so than when Clear image is called, we can restore the original pixels
		/// </summary>
		public void ReadClearingImage() {
			clearPixels = new byte[texWidth * texHeight * 4];

			// get our current texture into tex, is this needed?, also targettexture might be different..?
			// FIXME: usually target texture is same as drawingTexture..?
			drawingTexture.SetPixels32(((Texture2D)myRenderer.material.GetTexture(targetTexture)).GetPixels32());
			drawingTexture.Apply(false);

			int pixel = 0;
			Color32[] tempPixels = drawingTexture.GetPixels32();
			int tempCount = tempPixels.Length;

			for (int i = 0; i < tempCount; i++) {
				clearPixels[pixel] = tempPixels[i].r;
				clearPixels[pixel + 1] = tempPixels[i].g;
				clearPixels[pixel + 2] = tempPixels[i].b;
				clearPixels[pixel + 3] = tempPixels[i].a;
				pixel += 4;
			}
		}

		public void SetBrushSize(int newSize) {
			brushSize = (int)Mathf.Clamp(newSize, 1, 999);

			brushSizeX1 = brushSize << 1;
			brushSizeXbrushSize = brushSize * brushSize;
			brushSizeX4 = brushSizeXbrushSize << 2;
			brushSizeDiv4 = hiQualityBrush ? 0 : brushSize >> 2;

			UpdateLineModePreviewObjects();
		}

		public void SetDrawModeLine() {
			drawMode = DrawMode.ShapeLines;
		}

		public void SetDrawModeBrush() {
			drawMode = DrawMode.Default;
		}

		public void SetDrawModeFill() {
			drawMode = DrawMode.FloodFill;
		}

		public void SetDrawModeShapes() {
			drawMode = DrawMode.CustomBrush;
		}

		public void SetDrawModePattern() {
			drawMode = DrawMode.Pattern;
		}

		public void SetDrawModeEraser() {
			drawMode = DrawMode.Eraser;
		}

		/// <summary>
		/// returns current image (later: including all layers) as Texture2D
		/// </summary>
		/// <returns>Texture2D version of image</returns>
		public Texture2D GetCanvasAsTexture() {
			var image = new Texture2D((int)(texWidth / resolutionScaler), (int)(texHeight / resolutionScaler), TextureFormat.RGBA32, false);

			// TODO: combine layers to single texture
			image.LoadRawTextureData(pixels);
			image.Apply(false);

			return image;
		}

		/// <summary>
		/// returns screenshot as Texture2D
		/// </summary>
		/// <returns>Texture2D of screenshot</returns>
		public Texture2D GetScreenshot() {
			cam.Render();
			Mesh go_Mesh = GetComponent<MeshFilter>().mesh;
			var topLeft = cam.WorldToScreenPoint(go_Mesh.vertices[0]);
			var topRight = cam.WorldToScreenPoint(go_Mesh.vertices[3]);
			var bottomRight = cam.WorldToScreenPoint(go_Mesh.vertices[2]);
			var image = new Texture2D((int)(bottomRight.x - topLeft.x), (int)(bottomRight.y - topRight.y), TextureFormat.ARGB32, false);
			image.ReadPixels(new Rect(topLeft.x, topLeft.y, bottomRight.x, bottomRight.y), 0, 0);
			image.Apply(false);

			return image;
		}

		public int SnapToGrid(int val) {
			return (int)(val - val % gridSize);
		}

		/// <summary>
		/// Converts pixel coordinate to world position
		/// </summary>
		/// <param name="x">x-position</param>
		/// <param name="y">y-position</param>
		/// <returns>World position</returns>
		public Vector3 PixelToWorld(int x, int y) {
			Vector3 pixelPos = new Vector3(x, y, 0);	// x,y = texture pixel pos

			float planeWidth = myRenderer.bounds.size.x;
			float planeHeight = myRenderer.bounds.size.y;

			float localX = ((pixelPos.x / texWidth) - 0.5f) * planeWidth;
			float localY = ((pixelPos.y / texHeight) - 0.5f) * planeHeight;

			return new Vector3(localX, localY, 0);
		}

		/// <summary>
		/// Call this to change color, so that other objects get the new color also
		/// </summary>
		/// <param name="newColor">the new colour you are changing the brush to</param>
		public void SetPaintColor(Color32 newColor) {
			paintColor = newColor;

			SetBrushAlphaStrength(brushAlphaStrength);

			alphaLerpVal = paintColor.a / brushAlphaStrengthVal;	// precalc

			UpdateLineModePreviewObjects();
		}

		/// <summary>
		/// Set alpha power, good values are usually between 0.1 to 0.001
		/// </summary>
		/// <param name="val"></param>
		public void SetBrushAlphaStrength(float val) {
			brushAlphaStrengthVal = 255f / val;
		}

		void UpdateLineModePreviewObjects() {
			if (lineRenderer) {
				lineRenderer.SetColors(paintColor, paintColor);
				lineRenderer.SetWidth(brushSize * 2f / resolutionScaler, brushSize * 2f / resolutionScaler);
			}

			if (previewLineCircleEnd) {
				previewLineCircleEnd.GetComponent<SpriteRenderer>().color = paintColor;
				previewLineCircleEnd.transform.localScale = Vector3.one * brushSize * 0.8f / resolutionScaler;
			}

			if (previewLineCircleStart) {
				previewLineCircleStart.GetComponent<SpriteRenderer>().color = paintColor;
				previewLineCircleStart.transform.localScale = Vector3.one * brushSize * 0.8f / resolutionScaler;
			}
		}

		byte ByteLerp(byte value1, byte value2, float amount) {
			return (byte)(value1 + (value2 - value1) * amount);
		}

		/// <summary>
		/// Assigns new mask layer image
		/// </summary>
		/// <param name="newTexture"></param>
		public void SetMaskImage(Texture2D newTexture) {
			// Check if we have correct material to use mask image (layer)
			if (myRenderer.material.name.StartsWith("CanvasWithAlpha") || GetComponent<Renderer>().material.name.StartsWith("CanvasDefault")) {
				// FIXME: this is bit annoying to compare material names..
				Debug.LogWarning("CanvasWithAlpha and CanvasDefault materials do not support using MaskImage (layer). Disabling 'useMaskImage'");
				Debug.LogWarning("CanvasWithAlpha and CanvasDefault materials do not support using MaskImage (layer). Disabling 'useMaskLayerOnly'");
				useMaskLayerOnly = false;
				useMaskImage = false;
				maskTex = null;
			} else { //material is ok
				maskTex = newTexture;
				texWidth = newTexture.width;
				texHeight = newTexture.height;
				myRenderer.material.SetTexture("_MaskTex", newTexture);

				ReadMaskImage();

				textureNeedsUpdate = true;
			}
		}   // SetMaskImage

		/// <summary>
		/// Assigns new canvas image
		/// </summary>
		/// <param name="newTexture"></param>
		public void SetCanvasImage(Texture2D newTexture) {
			// NOTE: if new texture is different size, problems will occur when drawing
			myRenderer.material.SetTexture(targetTexture, newTexture);

			InitializeEverything();
		}

		public void SetPanZoomMode(bool state) {
			isZoomingOrPanning = state;
			this.enabled = isZoomingOrPanning ? false : true;	// Disable Update() loop from this script, if zooming or panning
		}

		// cleaning up buffers - https://github.com/unitycoder/UnityMobilePaint/issues/10
		void OnDestroy() {
			if (drawingTexture != null) { Texture2D.DestroyImmediate(drawingTexture, true); }
			pixels = null;
			maskPixels = null;
			clearPixels = null;
			lockMaskPixels = null;
			if (undoEnabled) { undoPixels.Clear(); }
		}
	}	// class
}	// namespace