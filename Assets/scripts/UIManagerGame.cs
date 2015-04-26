using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerGame : UIManager {
	private TaichoGameGrid grid;
	private Button unstackButton;
	private Button gameMenuButton; 
	public AudioManager audioManager;


	private bool isShowMenu = false;

	// Use this for initialization
	void Start () {
//		hideMenu ();
		if (this.grid == null && GameObject.FindGameObjectWithTag("TaichoGameGrid") != null) {
			this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		}
		this.isShowMenu = false;
		updateMusicIcon ();
		refreshMenu ();

	}
	
	// Update is called once per frame
	void Update () {
		//make sure these values are set
		if (this.gameMenuButton == null) {
			this.gameMenuButton = GameObject.FindGameObjectWithTag("GameMenuButton").GetComponent<Button> ();
		}
		if (this.grid == null) {
			this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		}
		if (this.unstackButton == null) {
			//need to use findGameObjectWi... because explicitly linking it in GUI does not seem able to disable button, so member is private
			this.unstackButton = GameObject.FindGameObjectWithTag("UnstackButton").GetComponent<Button> ();
		}
		if (this.grid != null && this.grid.shouldUnstackButtonBeEnabled()) {
			this.unstackButton.GetComponent<Button> ().interactable = true;
		} else {
			this.unstackButton.GetComponent<Button> ().interactable = false;
		}
	}

	public void LoadTutorial () {
		Debug.Log ("TODO make tutorial");
	}

	public void LoadSettings () {
		Debug.Log ("TODO make settings page");
	}

	public void showStartingMenu () {
		Application.LoadLevel ("MenuScene");
	}

	public void ShowGameMenu () {
		Debug.Log ("Showing Game Menu");

		this.isShowMenu = !this.isShowMenu;
		refreshMenu ();
	}

	public void OnUnstackPressed () {
		Debug.Log ("Unstack button pressed");
		this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		this.grid.onUnstackButtonClicked ();
	}

	public void SoundControlButtonPressed () {
		Debug.Log ("Sound Control button pressed");

		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = false;

		this.audioManager.BackgroundAudio = !this.audioManager.BackgroundAudio;
		updateMusicIcon ();
		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = true;
		
	}

	private void updateMusicIcon () {
		if (this.audioManager.BackgroundAudio) {
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = (Sprite)Resources.LoadAssetAtPath ("Assets/Resources/Images/myMusicIcon.png", typeof(Sprite));
		} else {
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = (Sprite)Resources.LoadAssetAtPath ("Assets/Resources/Images/myMusicIconOff.png", typeof(Sprite));
		}
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
