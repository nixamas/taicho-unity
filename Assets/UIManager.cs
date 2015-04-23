using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	private TaichoGameGrid grid;
	private Button unstackButton;
	private Button gameMenuButton; 
	private bool audioEnabled = true;

	private bool isShowMenu = false;

	// Use this for initialization
	void Start () {
		hideMenu ();
	}
	
	// Update is called once per frame
	void Update () {
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
		this.isShowMenu = !this.isShowMenu;
		refreshMenu ();
	}

	public void OnUnstackPressed () {
		Debug.Log ("Unstack button pressed");
		grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		grid.onUnstackButtonClicked ();
	}

	public void SoundControlButtonPressed () {
		Debug.Log ("Sound Control button pressed");
		audioEnabled = !audioEnabled;
		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = false;
		if (audioEnabled) {
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = (Sprite)Resources.LoadAssetAtPath ("Assets/Resources/Images/myMusicIconOff.png", typeof(Sprite));
		} else {
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = (Sprite)Resources.LoadAssetAtPath ("Assets/Resources/Images/myMusicIcon.png", typeof(Sprite));
		}
		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = true;
		
	}

	private void refreshMenu () {
		if (this.isShowMenu) {
			showMenu ();
		} else {
			hideMenu ();
		}
	}

	private void hideMenu () {
		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().image.enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().image.enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().image.enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = false;
		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = false;
	}

	private void showMenu () {
		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().image.enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().image.enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().image.enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = true;
		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = true;
	}
}
