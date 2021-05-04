
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this script controlls the intro tutorial
/// </summary>
public class TutorialManager : MonoBehaviour {

	GameObject Canvas, GalleryScene;
	GameObject TutorialUI, GalleryUI, TableUI, PaintUICanvas, CameraCanvas;
	GameObject Camera, TransitionController;
	GameObject Curator, CuratorImage, CuratorBox, DialogueText;
	GameObject NextButton, YesButton, NoButton, OfficeButton;
	GameObject LookPrompts, Black, Transparent;
	GameObject TutorialObjects, Easel, Table, BeginningDisplay;
	GameObject Map, MapStill, MapStillDraw, MapFreeDraw, MapOffice, MapEntrance, MapPortrait;
	GameObject PaintTutorial, PaintTutorial1, PaintTutorial2, PaintTutorial3, PaintTutorial4;

	private Animator CuratorAnim, CameraAnim, TranAnim, LookPromptsAnim, TutorialObjAnim, PaintCanvasAnim, CameraCanvasAnim, TableUIAnim, MapAnim, PaintTutorialAnim;

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

		//PlayerPrefs.SetInt("intro", 0);    // remove later 
		//PlayerPrefs.SetInt("banana", 0);    // remove later 
		//PlayerPrefs.SetInt("apple", 0);    // remove later

		//PlayerPrefs.SetInt("portraitLevel", 0);    // remove later

		//Debug.Log(PlayerPrefs.GetInt("intro") + " i");
		//Debug.Log(PlayerPrefs.GetInt("banana") + " b");
		//Debug.Log(PlayerPrefs.GetInt("apple") + " a");

		if (PlayerPrefs.GetInt("intro") == 0) {
			/// we need a title scene

			///  if the intro and therefore the tutprial has been completed at the begining of the game,
			///  how do you guys want the recap tutorial to work? do you want it to start again from the point
			///  the player clicks the cross when he asks "do you know how to paint?" ?


			if (SceneManager.GetActiveScene().name == "tutorial test") {
				tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
				cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();
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
				Black = Curator.transform.Find("Black").gameObject;

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
					TutorialObjAnim.Play("TableTutorialIn");
					CameraAnim.Play("CameraTable");
					CuratorAnim.Play("CuratorT");
				} else if (PlayerPrefs.GetInt("apple") == 1) {
					Map = TutorialUI.transform.Find("Map Tutorial").gameObject;
					MapStill = Map.transform.Find("Map Tutorial Still").gameObject;
					MapStillDraw = Map.transform.Find("Map Tutorial Still Draw").gameObject;
					MapFreeDraw = Map.transform.Find("Map Tutorial Free Draw").gameObject;
					MapOffice = Map.transform.Find("Map Tutorial Office").gameObject;
					MapAnim = Map.gameObject.GetComponent<Animator>();
					TutorialObjAnim.Play("TableTutorialOut");
					CameraAnim.Play("CameraTable");
					CuratorAnim.Play("CuratorB");
					YesButton.gameObject.SetActive(false);
					NoButton.gameObject.SetActive(false);
				}
			} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
				tutorialDialogeManager = GameObject.Find("dialoge manager").GetComponent<TutorialDialogeManager>();
				cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();
				Canvas = GameObject.Find("Main Canvas");
				TransitionController = GameObject.Find("Transition Controller").gameObject;
				cameraShutterScript = TransitionController.GetComponent<CameraShutterScript>();
				Camera = GameObject.Find("Main Camera");
				Curator = GameObject.Find("Curator");
				PaintUICanvas = GameObject.Find("Paint Canvas");
				CuratorAnim = Curator.gameObject.GetComponent<Animator>();
				PaintCanvasAnim = PaintUICanvas.gameObject.GetComponent<Animator>();
				TranAnim = TransitionController.GetComponent<Animator>();

				CameraCanvas = Camera.transform.Find("Canvas").gameObject;
				//TopButton = CameraCanvas.transform.Find("Button").gameObject;

				if (PlayerPrefs.GetInt("banana") == 0) {
					CuratorAnim.Play("CuratorB");

				} else if (PlayerPrefs.GetInt("apple") == 0) {
					CuratorAnim.Play("CuratorT");
					PaintCanvasAnim.Play("ShowLeftButtons");
					CameraCanvas.gameObject.SetActive(true);
				}
			} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
				cameraShutterScript = GameObject.Find("Transition Controller").GetComponent<CameraShutterScript>();
				Canvas = GameObject.Find("Main Canvas");
				TutorialUI = Canvas.transform.Find("Tutorial UI").gameObject;
				tutorialDialogeManager = TutorialUI.GetComponent<TutorialDialogeManager>();
				Curator = TutorialUI.transform.Find("Curator").gameObject;
				CuratorAnim = Curator.gameObject.GetComponent<Animator>();
				Transparent = Canvas.transform.Find("Transparent").gameObject;
				OfficeButton = GameObject.Find("Tutorial");

				

				if (PlayerPrefs.GetInt("portraitLevel") == 1) {
					Map = TutorialUI.transform.Find("Map Tutorial").gameObject;
					MapPortrait = Map.transform.Find("Map P Portrait").gameObject;
					MapEntrance = Map.transform.Find("Map P Entrance").gameObject;
					MapOffice = Map.transform.Find("Map P Office").gameObject;
					MapAnim = Map.gameObject.GetComponent<Animator>();
				

					TutorialUI.gameObject.SetActive(true);
					Transparent.gameObject.SetActive(true);

					TutorialDialogeManager.sentenceNumber = 52;
					CuratorAnim.Play("CuratorShowToRight");
					//			tutorialDialogeManager.StartDialogue();

				}
			}
		} else {
			/// play the scene from after the player says no to "do you know how to paint"	
			/// 


		}
		Debug.Log(TutorialDialogeManager.sentenceNumber);
	}

	public void NextButtonClicked() {
		Debug.Log("next" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 2) {
				CuratorAnim.Play("BoxHideInCentre");
				//cameraShutterScript.BlinkWake();
				//CameraAnim.Play("CameraWakeUp");
			} else if (TutorialDialogeManager.sentenceNumber == 4) {
				CuratorAnim.Play("CuratorCtoTNextD");
				LookPrompts.gameObject.SetActive(true);
				LookPromptsAnim.Play("LeftRightLookPrompt");
				Camera.GetComponent<Animator>().applyRootMotion = true; // Neccesary for Player to look around
			} else if (TutorialDialogeManager.sentenceNumber == 5) {
				Camera.GetComponent<Animator>().applyRootMotion = false;
				CuratorAnim.Play("CuratorTtoC");
				LookPrompts.gameObject.SetActive(false);
			} else if (TutorialDialogeManager.sentenceNumber == 7) {
				CuratorAnim.Play("CuratorCNextHYesSNoS");
			} else if (TutorialDialogeManager.sentenceNumber == 9) {
				/// end intro without tutorial 
				CuratorAnim.Play("CuratorHideToLeft");
				PlayerPrefs.SetInt("intro", 1);
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("BlinkClose");
			} else if (TutorialDialogeManager.sentenceNumber == 10) {
				CuratorAnim.Play("CuratorCtoTNextH");
				Easel.gameObject.SetActive(true);
				TutorialObjAnim.Play("EaselShowToRight");

			} else if (TutorialDialogeManager.sentenceNumber == 13) {
				TableUI.gameObject.SetActive(true);
				CuratorAnim.Play("CuratorTBoxHNextH");
				TableUIAnim.Play("TableUIShow");
			}

		} else if (SceneManager.GetActiveScene().name == "tutorial test" && PlayerPrefs.GetInt("banana") == 1 && PlayerPrefs.GetInt("apple") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 27) {
				TableUI.gameObject.SetActive(true);
				CuratorAnim.Play("CuratorTtoTI");
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
				MapAnim.Play("MapShowRestricted");
			} else if (TutorialDialogeManager.sentenceNumber == 38) {
				MapAnim.Play("MapShowStillDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 39) {
				MapAnim.Play("MapShowFreeDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 40) {
				MapAnim.Play("MapShowOffice");
			} else if (TutorialDialogeManager.sentenceNumber == 41) {
				MapAnim.Play("MapTutorialOut");
			} else if (TutorialDialogeManager.sentenceNumber == 42) {
				CuratorAnim.Play("CuratorBHideToLeft");
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("BlinkClose");
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene" && PlayerPrefs.GetInt("banana") == 0) {
			if (TutorialDialogeManager.sentenceNumber == 15) {
				TutorialDialogeManager.sentenceNumber += 1;
			} else if (TutorialDialogeManager.sentenceNumber == 18) {
				CameraCanvas.gameObject.SetActive(true);
				//	CameraCanvasAnim.Play("UIShowTopButton");/////////////////////////////////////////////////////////////////////////////////
				CuratorAnim.Play("CuratorBNextH");
			} else if (TutorialDialogeManager.sentenceNumber == 21) {
				PaintCanvasAnim.Play("ShowLeftButtons");
			} else if (TutorialDialogeManager.sentenceNumber == 24) {
				PaintCanvasAnim.Play("MakeFinishInteractableB");
				CuratorAnim.Play("CuratorBtoBI");
			} else if (TutorialDialogeManager.sentenceNumber == 45 || TutorialDialogeManager.sentenceNumber == 47) {
				CuratorAnim.Play("CuratorCtoBI");
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
				CuratorAnim.Play("CuratorTtoTI");
				PaintCanvasAnim.Play("MakeFinishInteractableA");
			} else if (TutorialDialogeManager.sentenceNumber == 49) {
				CuratorAnim.Play("CuratorCtoTI");
				TutorialDialogeManager.sentenceNumber = 33;
			} else if (TutorialDialogeManager.sentenceNumber == 50) {
				CuratorAnim.Play("CuratorCtoTI");
				TutorialDialogeManager.sentenceNumber = 33;
			}
		} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
			if (TutorialDialogeManager.sentenceNumber == 53) {
				CuratorAnim.Play("CuratorCtoB");
				Map.gameObject.SetActive(true);
				MapAnim.Play("MapTutorialIn");
			} else if (TutorialDialogeManager.sentenceNumber == 54) {
				MapAnim.Play("MapPPortrait");
			} else if (TutorialDialogeManager.sentenceNumber == 55) {
				MapAnim.Play("MapPShowEntrance");
			} else if (TutorialDialogeManager.sentenceNumber == 58) {
				MapAnim.Play("MapPShowOffice");
			} else if (TutorialDialogeManager.sentenceNumber == 59) {
				MapAnim.Play("MapPOfficeOut");
				CuratorAnim.Play("CuratorBHideToLeft");
				Transparent.gameObject.SetActive(false);
			} else if (TutorialDialogeManager.sentenceNumber == 62) {
				CuratorAnim.Play("CuratorHideToLeft");
			} else if (TutorialDialogeManager.sentenceNumber == 66) {
				CuratorAnim.Play("CuratorCtoB");
				PaintTutorial.gameObject.SetActive(true);
				PaintTutorialAnim.Play("PaintT1");
			} else if (TutorialDialogeManager.sentenceNumber == 67) {
				PaintTutorialAnim.Play("PaintT2");
			} else if (TutorialDialogeManager.sentenceNumber == 68) {
				PaintTutorialAnim.Play("PaintT3");
			} else if (TutorialDialogeManager.sentenceNumber == 69) {
				PaintTutorialAnim.Play("PaintT4");
			} else if (TutorialDialogeManager.sentenceNumber == 70) {
				PaintTutorialAnim.Play("PaintT4Out");
				Map.gameObject.SetActive(true);
				MapAnim.Play("MapOTShowStillLifeDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 71) {
				MapAnim.Play("MapOTShowFreeDraw");
			} else if (TutorialDialogeManager.sentenceNumber == 72) {
				MapAnim.Play("MapOTShowEntrance");
			} else if (TutorialDialogeManager.sentenceNumber == 74) {
				MapAnim.Play("MapPShowOffice");
			} else if (TutorialDialogeManager.sentenceNumber == 75) {
				MapAnim.Play("MapPOfficeOut");
				CuratorAnim.Play("CuratorBHideToLeft");
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void YesButtonClick() {
		Debug.Log("yes" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test") {
			if (TutorialDialogeManager.sentenceNumber == 8) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
			} else if (TutorialDialogeManager.sentenceNumber == 14) {
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("Camera Shutter Close Ani");
			} else if (TutorialDialogeManager.sentenceNumber == 28) {
				TransitionController.gameObject.SetActive(true);
				TranAnim.Play("Camera Shutter Close Ani");
			}

		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			if (TutorialDialogeManager.sentenceNumber == 44 || TutorialDialogeManager.sentenceNumber == 48) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
				TutorialDialogeManager.sentenceNumber += 1;
			}
		} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
			if (TutorialDialogeManager.sentenceNumber == 61) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
				TutorialDialogeManager.sentenceNumber = 65;
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void NoButtonClick() {
		Debug.Log("no" + TutorialDialogeManager.sentenceNumber);

		if (SceneManager.GetActiveScene().name == "tutorial test") {
			if (TutorialDialogeManager.sentenceNumber == 8) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
				tutorialDialogeManager.StartDialogue();
			}
		} else if (SceneManager.GetActiveScene().name == "StillLifeTutorialPaintScene") {
			if (TutorialDialogeManager.sentenceNumber == 44 || TutorialDialogeManager.sentenceNumber == 48) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
			}

		} else if (SceneManager.GetActiveScene().name == "GalleryScene") {
			if (TutorialDialogeManager.sentenceNumber == 61) {
				CuratorAnim.Play("CuratorCNextSYesHNoH");
			}
		}
		tutorialDialogeManager.StartDialogue();
	}

	public void CanvasButtonClick() {
		if (TutorialDialogeManager.sentenceNumber == 12) {
			CameraAnim.Play("CameraEaselToTable");
			CuratorAnim.Play("CuratorTNextS");

		}
		tutorialDialogeManager.StartDialogue();
	}
	public void TopButtonClick() {
		//Debug.Log("top" + TutorialDialogeManager.sentenceNumber);

		if (TutorialDialogeManager.sentenceNumber == 19) {
			tutorialDialogeManager.StartDialogue();
			CuratorAnim.Play("CuratorBNextS");
		}
	}

	public void FinishedButtonClick() {
		//Debug.Log("fin" + TutorialDialogeManager.sentenceNumber);

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
			TutorialDialogeManager.sentenceNumber = 43;
			CuratorAnim.Play("CuratorBItoC");

		} else if (TutorialDialogeManager.sentenceNumber == 34) {
			TutorialDialogeManager.sentenceNumber = 47;
			CuratorAnim.Play("CuratorTItoC");
		}
		tutorialDialogeManager.StartDialogue();
	}


	public void OfficeButtonClick() {
		TutorialDialogeManager.sentenceNumber = 60;

		TutorialUI.gameObject.SetActive(true);
		Curator.gameObject.SetActive(true);
		CuratorAnim.Play("CuratorYNShowToRight");

		PaintTutorial = TutorialUI.transform.Find("Paint Tutorial").gameObject;
		PaintTutorial1 = PaintTutorial.transform.Find("Paint Tutorial 1").gameObject;
		PaintTutorial2 = PaintTutorial.transform.Find("Paint Tutorial 2").gameObject;
		PaintTutorial3 = PaintTutorial.transform.Find("Paint Tutorial 3").gameObject;
		PaintTutorial4 = PaintTutorial.transform.Find("Paint Tutorial 4").gameObject;
		PaintTutorialAnim = PaintTutorial.gameObject.GetComponent<Animator>();
		Map = TutorialUI.transform.Find("Map Tutorial").gameObject;
		MapPortrait = Map.transform.Find("Map OT StillLifeDraw").gameObject;
		MapEntrance = Map.transform.Find("Map OT FreeDraw").gameObject;
		MapOffice = Map.transform.Find("Map P Entrance").gameObject;
		MapAnim = Map.gameObject.GetComponent<Animator>();

		tutorialDialogeManager.StartDialogue();
	}


	public void Asleep() {
		Black.gameObject.SetActive(true);
		Curator.gameObject.SetActive(true);
		CuratorAnim.Play("BoxShowInCentre");
		BeginningDisplay.gameObject.SetActive(false);
		TransitionController.gameObject.SetActive(false);

	}

	public void WakeUp() {
		TransitionController.gameObject.SetActive(true);
		TranAnim.Play("BlinkWakeUp");
		CameraAnim.Play("CameraWakeUp");
	}

	public void IsAwake() {
		Destroy(Black);
		CuratorAnim.Play("CuratorShowToRight");

		NextButton.gameObject.SetActive(true);
		CuratorImage.gameObject.SetActive(true);
	}

	public void MoveTable() {
		TutorialObjAnim.Play("TableTutorialMoveIn");
	}
}