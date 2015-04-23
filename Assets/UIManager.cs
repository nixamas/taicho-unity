using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	private string gameMenuBackButtonTag = "GameMenuBackButton";
	private string gameMenuRestartButtonTag = "GameMenuRestartButton";

	private TaichoGameGrid grid;
	private Button unstackButton;
	private Button gameMenuButton;
//	private Image gameMenuDropDown;

	private bool showMenu = false;

	// Use this for initialization
	void Start () {
		GameObject.FindGameObjectWithTag (gameMenuBackButtonTag).GetComponent<Button> ().enabled = false;
		GameObject.FindGameObjectWithTag (gameMenuRestartButtonTag).GetComponent<Button> ().enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
//		if (gameMenuDropDown == null) {
		
		if (unstackButton == null) {
			unstackButton = GameObject.FindGameObjectWithTag("UnstackButton").GetComponent<Button> ();
		}
		if (gameMenuButton == null) {
			gameMenuButton = GameObject.FindGameObjectWithTag("UnstackButton").GetComponent<Button> ();
		}
		if (grid == null) {
			grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		}
		if (grid != null && grid.shouldUnstackButtonBeEnabled()) {
			unstackButton.GetComponent<Button> ().interactable = true;
		} else {
			unstackButton.GetComponent<Button> ().interactable = false;
		}
	}

	public void StartGame() {
		Application.LoadLevel ("GameBoardScene");
	}

	public void LoadTutorial () {
		Debug.Log ("TODO make tutorial");
	}

	public void LoadSettings () {
		Debug.Log ("TODO make settings page");
	}

	public void ShowGameMenu () {
		Debug.Log ("Showing Game Menu");
//		if (gameMenuDropDown == null) {
//			gameMenuDropDown = ;
//		}
		GameObject.FindGameObjectWithTag("GameMenuDropDown").GetComponent<Image> ().enabled = this.showMenu;
		//			gameMenuDropDown.enabled = false;
		//			foreach (Button button in gameMenuDropDown.GetComponentsInChildren<Button> ()) {
		//				button.enabled = false;
		//			}
		//		}
		GameObject.FindGameObjectWithTag (gameMenuBackButtonTag).GetComponent<Button> ().enabled = this.showMenu;
		GameObject.FindGameObjectWithTag (gameMenuRestartButtonTag).GetComponent<Button> ().enabled = this.showMenu;
		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = true;
	}

	public void OnUnstackPressed () {
		Debug.Log ("Unstack button pressed");
		grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		grid.onUnstackButtonClicked ();
	}
}
