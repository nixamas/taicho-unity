using UnityEngine;
using System.Collections;
using System;

public class LobbyScrollButton : MonoBehaviour {

	public string roomName;
	public string actorTurn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onLobbyScrollButtonClick () {
		Debug.Log ("onLobbyScrollButtonClick [roomName::"+roomName+"]    [actorTurn::"+actorTurn+"]");


		NetworkManager.instance.rejoinRoomByName(roomName, actorTurn);
	}

	public void onLobbyRoomGameButtonClick () {
		Debug.Log ("onLobbyRoomGameButtonClick [roomName::"+roomName+"]");

		NetworkManager.instance.joinRoomByName(false, roomName);
	}
}
