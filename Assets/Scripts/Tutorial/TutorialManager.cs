
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this script controlls the intro tutorial
/// </summary>
public class TutorialManager : MonoBehaviour {

	 GameObject Canvas, GalleryScene;
	 GameObject TutorialUI, GalleryUI, TableUI, PaintUICanvas;
	 GameObject Camera, TransitionController; // Pannel;
	 GameObject Curator, CuratorImage, CuratorBox, DialogueText;
	 GameObject NextButton, YesButton, NoButton;
	 GameObject LookPrompts;
	 GameObject TutorialObjects, Easel, Table, BeginningDisplay;
	 GameObject Map, MapStill, MapStillDraw, MapFreeDraw, MapOffice;

	private Animator CuratorAnim, CameraAnim, TranAnim, LookPromptsAnim, TutorialObjAnim, PaintCanvasAnim, TableUIAnim, MapAnim;

	TutorialDialogeManager tutorialDialogeManager;
	CameraShutterScript cameraShutterScript;

	/// <summary>
	/// the intro tutorial walks the player thorugh how to paint and how to navigate the museum while setting the sinario
	/// first the player has the opotunity to look around
	/// then they are shown how to paint a banana using the two painting methods
	/// then they do the same with an apple but with colour and shape changes to the brush
	/// and finaly they are shown the key locations on the map
	/// once the intro has been completed it will not play again
	/// if the intro is stopped part way through and the game reopened the text wont line up with the scene
	/// </summary>
	private void Awake() {

		PlayerPrefs.SetInt("intro", 0);    // remove later 
		PlayerPrefs.SetInt("banana", 0);    // remove later 
		PlayerPrefs.SetInt("apple", 0);    // remove later

		//Debug.Log(PlayerPrefs.GetInt("intro") + " i");
		//Debug.Log(PlayerPrefs.GetInt("banana") + " b");
		//Debug.Log(PlayerPrefs.GetInt("apple") + " a");

		if (PlayerPrefs.GetInt("intro") == 0) {
			/// we need a title scene

			///  if the intro and therefore the tutprial has been completed at the begining of the game,
			///  how do you guys want the recap tutorial to work? do you want it to start again from the point
			///  the player clicks the cross when he asks "do you know how to paint?" ?

			tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
			cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();

			if (SceneManager.GetActiveScene().name == "tutorial test") {
				Canvas = GameObject.Find("Main Canvas");
				TransitionController = GameObject.Find("Transition Controller").gameObject;
				TutorialUI = Canvas.transform.Find("Tutorial UI").gameObject;
				TableUI = Canvas.transform.Find("Table UI").gameObject;
				GalleryScene = GameObject.Find("Gallery Scene");
				TutorialObjects = GalleryScene.transform.Find("Tutorial Objects").gameObject;
				Table = TutorialObjects.transform.Find("Tutorial Table").gameObject;
				Easel = TutorialObjects.transform.Find("Tutorial Easel").gameObject;
				BeginningDisplay = TutorialObjects.transform.Find("Beginning Display").gameObject;
				TutorialUI.gameObject.SetActive(true);
				Camera = GameObject.Find("Main Camera");
				Curator = GameObject.Find("Curator");
				CuratorImage = GameObject.Find("personImage");
				CuratorBox = GameObject.Find("Box");
				DialogueText = GameObject.Find("Dialogue");
				NextButton = GameObject.Find("Next Button");
				YesButton = GameObject.Find("Yes Button");
				NoButton = GameObject.Find("No Button");
				LookPrompts = GameObject.Find("Look Prompts");

				CuratorAnim = Curator.gameObject.GetComponent<Animator>();
				CameraAnim = Camera.gameObject.GetComponent<Animator>();
				LookPromptsAnim = LookPrompts.gameObject.GetComponent<Animator>();
				TutorialObjAnim = TutorialObjects.gameObject.GetComponent<Animator>();
				TableUIAnim = TableUI.gameObject.GetComponent<Animator>();
				TranAnim = GameObject.Find("Transition Controller").GetComponent<Animator>();

				if (PlayerPrefs.GetInt("banana") == 0) {
					TutorialObjects.gameObject.SetActive(true);
					BeginningDisplay.gameObject.SetActive(true);
					Curator.gameObject.SetActive(false);
					CuratorImage.gameObject.SetActive(false);
					CuratorBox.gameObject.SetActive(false);
					NextButton.gameObject.SetActive(false);
					YesButton.gameObject.SetActive(false);
					NoButton.gameObject.SetActive(false);
					CameraAnim.Play("CameraCalmLookAround");
					TranAnim.Play("BlinkSleep");
				} else if (PlayerPrefs.GetInt("banana") == 1 && PlayerPrefs.GetInt("apple") == 0) {
					TutorialObjects.gameObject.SetActive(true);
					Easel.gameObject.SetActive(true);
					Table.gameObject.SetActive(true);
					NextButton.gameObject.SetActive(true);
					YesButton.gameObject.SetActive(false);
					NoButton.gameObject.SetActive(false);
					TutorialObjAnim.Play("EaselandTableIn");
					CameraAnim.Play("CameraTable");
					CuratorAnim.Play("CuratorB");
					TranAnim.Play("Camera Shutter Open Ani");
				} else if (PlayerPrefs.GetInt("apple") == 1) {
					Map = TutorialUI.transform.Find("Map Tutorial").gameObject;
					MapStill = Map.transform.Find("Map Tutorial Still").gameObject;
					MapStillDraw = Map.transform.Find("Map Tutorial Still Draw").gameObject;
					MapFreeDraw = Map.transform.Find("Map Tutorial Free Draw").gameObject;
					MapOffice = Map.transform.Find("Map Tutorial Office").gameObject;
					MapAnim = Map.gameObject.GetComponent<Animator>();
					TutorialObjAnim.Play("EaselandTable2In");
					CameraAnim.Play("CameraTable");
					CuratorAnim.Play("CuratorB");
					YesButton.gameObject.SetActive(false);
					NoButton.gameObject.SetActive(false);
				}
			} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {

				Canvas = GameObject.Find("Main Canvas");
				TransitionController = GameObject.Find("Transition Controller").gameObject;
				cameraShutterScript = TransitionController.GetComponent<CameraShutterScript>();
				Camera = GameObject.Find("Main Camera");
				Curator = GameObject.Find("Curator");
				PaintUICanvas = GameObject.Find("Paint Canvas");
				CuratorAnim = Curator.gameObject.GetComponent<Animator>();
				PaintCanvasAnim = PaintUICanvas.gameObject.GetComponent<Animator>();
				TranAnim = TransitionController.GetComponent<Animator>();

				if (PlayerPrefs.GetInt("banana") == 0) {
					CuratorAnim.Play("CuratorB");

				} else if (PlayerPrefs.GetInt("apple") == 0) {
					CuratorAnim.Play("CuratorT");
					PaintCanvasAnim.Play("ShowLeftButtons");
				}
			}
		} else {
			/// play the scene from after the player says no to "do you know how to paint"
			
		}
//		Debug.Log(TutorialDialogeManager.sentenceNumber);
	}

	public void NextButtonClicked() {
//		Debug.Log("next" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 2) {
				CuratorAnim.Play("BoxHideInCentre");
				cameraShutterScript.BlinkWake();
				CameraAnim.Play("CameraWakeUp");
			} else if (TutorialDialogeManager.sentenceNumber == 4) {
				CuratorAnim.Play("CuratorCToB");
				LookPrompts.gameObject.SetActive(true);
				LookPromptsAnim.Play("LeftRightLookPrompt");
                Camera.GetComponent<Animator>().applyRootMotion = true; // Neccesary for Player to look around
			} else if (TutorialDialogeManager.sentenceNumber == 5) {
                Camera.GetComponent<Animator>().applyRootMotion = false;
                CuratorAnim.Play("CuratorBToC");
				LookPrompts.gameObject.SetActive(false);
			} else if (TutorialDialogeManager.sentenceNumber == 7) {
				CuratorAnim.Play("NextHYesSNoS");
			} else if (TutorialDialogeManager.sentenceNumber == 9) {
				/// end intro without tutorial 
				CuratorAnim.Play("HideToLeft");
				PlayerPrefs.SetInt("intro", 1);
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("BlinkClose");
			} else if (TutorialDialogeManager.sentenceNumber == 10) {
				CuratorAnim.Play("CuratorCToBNH");
				Easel.gameObject.SetActive(true);
				TutorialObjAnim.Play("EaselShowToRight");
		
			} else if (TutorialDialogeManager.sentenceNumber == 13) {
				TableUI.gameObject.SetActive(true);
				CuratorAnim.Play("BoxBHNextH");
				TableUIAnim.Play("TableUIShow");
			}

		} else if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") == 1 && PlayerPrefs.GetInt("apple") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 27) {
				TableUI.gameObject.SetActive(true);
				CuratorAnim.Play("BoxBHNextH");
				TutorialObjAnim.Play("TableTutorialMoveOut");
				TableUIAnim.Play("TableUIShow");
			}

		} else if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("apple") == 1) {
			if (TutorialDialogeManager.sentenceNumber == 35) {
				Map.gameObject.SetActive(true);
				MapAnim.Play("MapTutorialIn");
			} else if (TutorialDialogeManager.sentenceNumber == 36) {
				MapAnim.Play("MapShowStill");
			} else if (TutorialDialogeManager.sentenceNumber == 37) {
				MapAnim.Play("MapShowStillDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 38) {
				MapAnim.Play("MapShowFreeDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 39) {
				MapAnim.Play("MapShowOffice");
			} else if (TutorialDialogeManager.sentenceNumber == 40) {
				MapAnim.Play("MapTutorialOut");
			} else if (TutorialDialogeManager.sentenceNumber == 41) {
				CuratorAnim.Play("CuratorBHideToLeft");
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("BlinkClose");
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene" && PlayerPrefs.GetInt("banana") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 15) {
				TutorialDialogeManager.sentenceNumber += 1;
			} else if (TutorialDialogeManager.sentenceNumber == 18) {
				PaintCanvasAnim.Play("ShowTopButton");
				CuratorAnim.Play("CuratorBHNext");
			} else if (TutorialDialogeManager.sentenceNumber == 21) {
				PaintCanvasAnim.Play("ShowLeftButtons");
			} else if (TutorialDialogeManager.sentenceNumber == 24) {
				PaintCanvasAnim.Play("MakeFinishInteractableB");
				CuratorAnim.Play("CuratorBToI");
			} else if (TutorialDialogeManager.sentenceNumber == 44 || TutorialDialogeManager.sentenceNumber == 45) {
				CuratorAnim.Play("CuratorCToI");
				TutorialDialogeManager.sentenceNumber = 24;
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene" && PlayerPrefs.GetInt("apple") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 29) {
				PaintCanvasAnim.Play("ShowColours");

			} else if (TutorialDialogeManager.sentenceNumber == 30) {
				/// change the next button to the colours and canvas
				//	CuratorAnim.Play("CuratorTHNext");

			} else if (TutorialDialogeManager.sentenceNumber == 31) {
				PaintCanvasAnim.Play("ShowBrushSize");
				//	CuratorAnim.Play("CuratorTSNext");

			} else if (TutorialDialogeManager.sentenceNumber == 32) {
				///change the next button to the brush size
				//	CuratorAnim.Play("CuratorTHNext");
			} else if (TutorialDialogeManager.sentenceNumber == 33) {
				CuratorAnim.Play("CuratorTtoI");
				PaintCanvasAnim.Play("MakeFinishInteractableA");
			} else if (TutorialDialogeManager.sentenceNumber == 48 || TutorialDialogeManager.sentenceNumber == 49) {
				CuratorAnim.Play("CuratorCToI");
				TutorialDialogeManager.sentenceNumber = 33;
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void YesButtonClick() {
		//Debug.Log("yes" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test") {
			if (TutorialDialogeManager.sentenceNumber == 8) {
				CuratorAnim.Play("NextSYesHNoH");
			} else if (TutorialDialogeManager.sentenceNumber == 14) {
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("Camera Shutter Close Ani");
			} else if (TutorialDialogeManager.sentenceNumber == 28) {
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("Camera Shutter Close Ani");
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			if (TutorialDialogeManager.sentenceNumber == 43 || TutorialDialogeManager.sentenceNumber == 47) {
				CuratorAnim.Play("NextSYesHNoH");
				TutorialDialogeManager.sentenceNumber += 1;
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void NoButtonClick() {
		//Debug.Log("no" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test") {
			if (TutorialDialogeManager.sentenceNumber == 8) {
				CuratorAnim.Play("NextSYesHNoH");
				tutorialDialogeManager.StartDialogue();
			}
		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			if (TutorialDialogeManager.sentenceNumber == 43 || TutorialDialogeManager.sentenceNumber == 47) {
				CuratorAnim.Play("NextSYesHNoH");
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void CanvasButtonClick() {
		if (TutorialDialogeManager.sentenceNumber == 12) {
			CameraAnim.Play("CameraEaselToTable");
			CuratorAnim.Play("CuratorBSNext");

		}
		tutorialDialogeManager.StartDialogue();
	}
	public void TopButtonClick() {
		//Debug.Log("top" + TutorialDialogeManager.sentenceNumber);

		if (TutorialDialogeManager.sentenceNumber == 19) {
			tutorialDialogeManager.StartDialogue();
			CuratorAnim.Play("CuratorBSNext");
		}
	}

	public void FinishedButtonClick() {
		Debug.Log("fin" + TutorialDialogeManager.sentenceNumber);

		if (PlayerPrefs.GetInt("banana") == 0) {
			PlayerPrefs.SetInt("banana", 1);
		} else if (PlayerPrefs.GetInt("apple") == 0) {
			PlayerPrefs.SetInt("apple", 1);
		}
		TransitionController.gameObject.SetActive(true);
		TranAnim.Play("Camera Shutter Close Ani");
	}

	public void CuratorButtonClick() {
		//Debug.Log("cur" + TutorialDialogeManager.sentenceNumber);

		if (TutorialDialogeManager.sentenceNumber == 25) {
			TutorialDialogeManager.sentenceNumber = 42;
			CuratorAnim.Play("CuratorIToCenter");

		} else if (TutorialDialogeManager.sentenceNumber == 34) {
			TutorialDialogeManager.sentenceNumber = 46;
			CuratorAnim.Play("CuratorIToCenter");
		}
		tutorialDialogeManager.StartDialogue();
	}


	public void Asleep() {
		Curator.gameObject.SetActive(true);
		CuratorAnim.Play("BoxShowInCentre");
		BeginningDisplay.gameObject.SetActive(false);
	}

	public void WakeUp() {
		CuratorImage.gameObject.SetActive(true);
		CuratorAnim.Play("BoxShowToRight");
		NextButton.gameObject.SetActive(true);
//		Blink.gameObject.SetActive(false);
	}

	public void MoveTable() {
		TutorialObjAnim.Play("TableTutorialMoveIn");
	}
}