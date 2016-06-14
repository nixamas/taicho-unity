using ExitGames.Client.Photon.LoadBalancing;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManagerLobby : UIManager {

	public Button [] menuButtons;
//	public Text userNameInputText;
	public Text roomNameInputText;
	public ScrollRectContent savedRoomsSelect;
	public ScrollRectContent lobbyRoomsSelect;
	public StatusLabel userStatusLabel;


	// Use this for initialization
	void Start () {
		foreach (Button button in menuButtons) {
			button.GetComponent<Image> ().color = TaichoColors.TAICHO_MENU_BUTTONS;
		}
	}

	private int currentSavedGameCount = 0, currentLobbyRoomCount = 0;
	// Update is called once per frame
	void Update () {
		if (NetworkManager.instance != null  
		    && NetworkManager.instance.GameInstance != null 
			    && NetworkManager.instance.GameInstance.NickName != null) {
			userStatusLabel.networkUserName = NetworkManager.instance.GameInstance.NickName;
		}

		if (NetworkManager.instance.GameInstance != null && NetworkManager.instance.GameInstance.SavedGames != null) {
			int newSavedGameCount = NetworkManager.instance.GameInstance.SavedGames.Count;
			if (currentSavedGameCount != newSavedGameCount) {
				savedRoomsSelect.clearList ();
				foreach (KeyValuePair<string, int> savedRoom in NetworkManager.instance.GameInstance.SavedGames) {
					string roomName = savedRoom.Key;
					int actorNumber = savedRoom.Value;
					
					Debug.Log ("room name ::" + roomName + " -- actor number ::" + actorNumber);
					savedRoomsSelect.addItemToList(roomName, actorNumber.ToString ());

					currentSavedGameCount = newSavedGameCount;
				}
			}
		}

		if (NetworkManager.instance.GameInstance != null && NetworkManager.instance.GameInstance.RoomInfoList != null) {
			int newLobbyRoomCount = NetworkManager.instance.GameInstance.RoomInfoList.Values.Count;
			if (currentLobbyRoomCount != newLobbyRoomCount) {
				lobbyRoomsSelect.clearList ();
				foreach (RoomInfo roomInfo in NetworkManager.instance.GameInstance.RoomInfoList.Values) {
					Debug.Log ("Lobby Room name :: [" + roomInfo.Name + "]");

					lobbyRoomsSelect.addItemToList(roomInfo.Name, "");

					currentLobbyRoomCount = newLobbyRoomCount;
				}
			}
		}


	}



	public void OnGUI()	{

		this.networkManager.GuiInLobby ();
	}

	public void updateRoomNameInfo () {
		NetworkManager.instance.roomToJoinName = roomNameInputText.GetComponent<Text> ().text;
	}

	public void startOnlineTurnbasedGame () {
		Debug.Log ("TODO start online game");

		Debug.Log ("SET NetworkManager.instance.myUserName ["+NetworkManager.instance.myUserName+"]");

	}

	public void onRoomNameEntered () {
		string roomName = roomNameInputText.text;
		Debug.Log("onRoomNameEntered ["+roomName+"]");
		NetworkManager.instance.roomToJoinName = roomName;
		NetworkManager.instance.createTurnBasedRoom();
		Application.LoadLevel ("GameBoardScene");
	}


}
