using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

	private const string roomName = "BewareTheInvisibleDragons";
	const string VERSION = "v4.2";
		
	public bool localDevelopment = false;

	void Start () {
		if (!localDevelopment) {
			PhotonNetwork.ConnectUsingSettings (VERSION);
		} else {
//			Debug.Log ("development mode");
//			Instantiate (carPrefab, 
//			             spawnPoint.position,
//			             spawnPoint.rotation);
		}
	}

	void OnJoinedLobby () {
		Debug.Log ("In the lobby bitches");
		RoomOptions roomOptions = new RoomOptions () { isVisible = false, maxPlayers = 2};
		PhotonNetwork.JoinOrCreateRoom (roomName, roomOptions, TypedLobby.Default);
	}

	void OnJoinedRoom() {
		Debug.Log ("Im joining!!");
		/* upon user joining the room, spawn them at the spawnpoint */
		Debug.Log ("should be joined");
	}
}
