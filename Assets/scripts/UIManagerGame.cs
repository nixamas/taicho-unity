using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ExitGames.Client.Photon;
using ExitGames.Client.Photon.LoadBalancing;

public class UIManagerGame : UIManager {
	public TaichoGameGrid grid;
	private Button unstackButton;
	private Button gameMenuButton; 
	public AudioManager audioManager;
	public Sprite musicOnIcon;
	public Sprite musicOffIcon;


	private bool isShowMenu = false;

	// Use this for initialization
	void Start () {
		Debug.Log("NetworkManager.instance.myUserName ["+NetworkManager.instance.myUserName+"]");
//		hideMenu ();
		if (this.grid == null && GameObject.FindGameObjectWithTag("TaichoGameGrid") != null) {
			this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		}
		this.isShowMenu = false;
		updateMusicIcon ();
		NetworkManager.instance.GameInstance.taichoGrid = grid;
//		refreshMenu ();

	}
	
	// Update is called once per frame
	void Update () {
		//make sure these values are set
		if (this.gameMenuButton == null) {
			this.gameMenuButton = GameObject.FindGameObjectWithTag("GameMenuButton").GetComponent<Button> ();
		}

		updateUnstackButton ();
	}

	public void updateUnstackButton () {
		//need to use findGameObjectWi... because explicitly linking it in GUI does not seem able to disable button, so member is private
		this.unstackButton = GameObject.FindGameObjectWithTag("UnstackButton").GetComponent<Button> ();
		this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		if (this.grid != null && this.grid.shouldUnstackButtonBeEnabled()) {
			this.unstackButton.GetComponent<Button> ().interactable = true;
		} else {
			this.unstackButton.GetComponent<Button> ().interactable = false;
		}
	}

	public void OnGUI()
	{
		if (this.networkManager != null) {
			this.networkManager.GuiInGame ();
		} else {
			// TODO Maybe do something here
		}
//		string stateLabel = "***";
//		if (NetworkManager.instance.GameInstance.NickName != null)
//			stateLabel += "name: [" + NetworkManager.instance.GameInstance.NickName + "]  ";
//		if (NetworkManager.instance.roomToJoinName != null)
//			stateLabel += "RoomName: [" + NetworkManager.instance.roomToJoinName + "]  ";
//		if (NetworkManager.instance.GameInstance.NickName != null)
//			stateLabel += "state: [" + NetworkManager.instance.GameInstance.State.ToString() + "]  ";
//		GUILayout.Label(stateLabel);
//		Rect leftToolbar = new Rect(NetworkManager.instance.leftToolbar.x, NetworkManager.instance.leftToolbar.y, NetworkManager.instance.leftToolbar.width, Screen.height - NetworkManager.instance.leftToolbar.y);
//		GUILayout.BeginArea(leftToolbar);
//		GUI.skin.button.stretchWidth = false;
//		GUI.skin.button.fixedWidth = 150;
//		
//		if (NetworkManager.instance.GameInstance.CurrentRoom == null) {
//			Debug.LogError("Game Instance is null!!!!!?!?!?!?");
//		} else {
//			// we are in a room, so we can access CurrentRoom and it's Players
//			GUILayout.Label("In Room: " + NetworkManager.instance.GameInstance.CurrentRoom.Name);
//			
//			string interestingPropsAsString = NetworkManager.instance.FormatRoomProps();
//			if (!string.IsNullOrEmpty(interestingPropsAsString))
//			{
//				GUILayout.Label("Props: " + interestingPropsAsString);
//			}
//			
//			foreach (Player player in NetworkManager.instance.GameInstance.CurrentRoom.Players.Values)
//			{
//				if (player.ID == NetworkManager.instance.GameInstance.lastTurnPlayer)
//				{
//					GUILayout.Label(player.ToString() + " (played last)");
//				}
//				else
//				{
//					GUILayout.Label(player.ToString());
//				}
//			}
//		}
//		
//		GUILayout.Space(15);
//		
//		GUILayout.Label("Save the board by ending the turn.");
//		if (GUILayout.Button("End Turn " + NetworkManager.instance.GameInstance.turnNumber + " (Save)"))
//		{
//			NetworkManager.instance.GameInstance.SaveBoardAsProperty();
//		}
//		
//		GUILayout.FlexibleSpace();
//		
//		if (GUILayout.Button("Leave (return later)"))
//		{
//			NetworkManager.instance.saveAndLeaveRoom ();
//		}
//		if (GUILayout.Button("Abandon"))
//		{
//			NetworkManager.instance.GameInstance.OpLeaveRoom(false);
//		}
//		GUILayout.EndArea();
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

//	public void ShowGameMenu () {
//		Debug.Log ("Showing Game Menu");
//
//		this.isShowMenu = !this.isShowMenu;
//		refreshMenu ();
//	}

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
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = musicOnIcon;
		} else {
			GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().sprite = musicOffIcon;
		}
	}

//	private void refreshMenu () {
//		if (this.isShowMenu) {
//			showMenu ();
//		} else {
//			hideMenu ();
//		}
//	}

//	public void hideMenu () {
//		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().image.enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().image.enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().image.enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = false;
//		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = false;
//	}
//
//	private void showMenu () {
//		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().image.enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuBackButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().image.enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuRestartButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().image.enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuSoundControlButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = true;
//		GameObject.FindGameObjectWithTag ("GameMenuDropDown").GetComponent<Image> ().enabled = true;
//		GameObject.FindGameObjectWithTag ("AudioManagerIcon").GetComponent<Image> ().enabled = true;
//	}
}
