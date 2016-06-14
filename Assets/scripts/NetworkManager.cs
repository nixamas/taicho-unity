using ExitGames.Client.Photon.LoadBalancing;
using System.Collections.Generic;
using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Random = UnityEngine.Random;

public class NetworkManager : MonoBehaviour
{

	private static NetworkManager _instance;
	
	public static NetworkManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<NetworkManager>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}
	
	void Awake() 
	{
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}
	}
	
	public string myUserName {get; set;}
	public string roomToJoinName {get; set;}// = "BewareTheInvisibleDragons-0795";

	public NetworkTaichoGame GameInstance;
	private string AppId = "51c114bf-b337-4356-b852-c37e671e1f8f";            //set this in the editor in a scene!
	public Rect LobbyRect;  // set in inspector to position the lobby screen
	public Rect leftToolbar;  // set in inspector to position the lobby screen

	public bool SkipUpdate;
	public bool SetRandomRoomPropsAuto;
	private float timeToSend;
	public bool rejoiningTaichoGame = false;

	public bool displayDebugInformation;
	
	public void Start()
	{
		Debug.Log("Starting network manager");
		Application.runInBackground = true;
		CustomTypes.Register();
		
		leftToolbar = new Rect(leftToolbar.x, leftToolbar.y, leftToolbar.width, Screen.height - leftToolbar.y);
//		initializeGameInstance ();
	}

	public void initializeGameInstance () {
		Debug.Log("Initializing Game Instance");
		if (instance.GameInstance != null)
			return;
		instance.GameInstance = new NetworkTaichoGame();
		instance.GameInstance.AppId = instance.AppId;       // set in Inspector
		instance.GameInstance.AppVersion = "1.11";
		instance.GameInstance.NickName = !string.IsNullOrEmpty(instance.myUserName) ? instance.myUserName : "unityPlayer";
		Debug.Log ("Setting GameInstance NickName to " + instance.GameInstance.NickName);
		
		instance.GameInstance.OnStateChangeAction += instance.OnStateChanged;
		instance.GameInstance.ConnectToRegionMaster("EU");  // Turnbased games have to use this connect via Name Server
		
	}
	
	private void OnStateChanged(ClientState state)
	{
		if (state == ClientState.ConnectedToMaster)
		{
			instance.GetRoomsList();
		}
	}
	
	private void GetRoomsList()
	{
		Debug.Log("Getting Rooms list...");
		instance.GameInstance.OpWebRpc("GetGameList", new Dictionary<string, object>());
	}
	
	public void OnApplicationQuit()
	{
		if (instance.GameInstance != null && instance.GameInstance.loadBalancingPeer != null)
		{
			instance.GameInstance.Disconnect();
			instance.GameInstance.loadBalancingPeer.StopThread();
		}
		instance.GameInstance = null;
	}
	
	

	
	public void Update()
	{
		
		if (instance.GameInstance == null) return;
		if (instance.GameInstance.taichoGrid != null && instance.SetRandomRoomPropsAuto && timeToSend < Time.time)
		{
			timeToSend = Time.time + 0.1f;
			instance.GameInstance.OpSetCustomPropertiesOfRoom(new Hashtable() {{"t", Time.time}}, false);
		}
		
		instance.GameInstance.Service();
		
		// "back" button of phone will quit
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		} 
	}
	
	public void OnGUI()
	{
		if (string.IsNullOrEmpty(instance.AppId)) 
		{
			GUILayout.Label("You must enter your AppId from the Dashboard in the component: Scripts, DemoGUI.AppId before you can use instance demo.");
			return;
		}
		
		GUI.skin.button.stretchWidth = true;
		GUI.skin.button.fixedWidth = 0;
	}

	public void joinLobbyRoom (string userName) {
		instance.myUserName = userName;
		Debug.Log("Defined the user name :: " + instance.myUserName);
		initializeGameInstance ();
		Debug.Log ("Joining lobby room as " + instance.myUserName );
	}

	public void joinOrCreateRandomRoom () {
		instance.GameInstance.OpJoinRandomRoom(null, 0);
		Application.LoadLevel ("GameBoardScene");
	}

	public void joinRoomByName () {
		createTurnBasedRoom ();
		instance.GameInstance.OpJoinRoom(instance.roomToJoinName, -1);
		Application.LoadLevel ("GameBoardScene");
	}

	public void joinRoomByName (bool createRoom, string roomName) {
		if (createRoom) {
			createTurnBasedRoom ();
		}
		instance.GameInstance.OpJoinRoom(instance.roomToJoinName, -1);
		Application.LoadLevel ("GameBoardScene");
	}

	public void rejoinRoomByName (string roomName, string actorTurn) {
		Debug.Log ("Rejoining online game ["+roomName+"] -- actor turn ["+actorTurn+"]");
		instance.GameInstance.OpJoinRoom(roomName, Int32.Parse(actorTurn));
		instance.rejoiningTaichoGame = true;
		Application.LoadLevel ("GameBoardScene");
	}

	public void createTurnBasedRoom () {
		Debug.Log("NetworkManager.createTurnBasedRoom");
		instance.GameInstance.CreateTurnbasedRoom(instance.roomToJoinName);
		Debug.Log("Created turn based room");
	}

	public void saveAndLeaveRoom() {
		Debug.Log("Saving and leaving the room");
		instance.GameInstance.SaveBoardAsProperty();
		instance.GameInstance.OpLeaveRoom(true);
		Application.LoadLevel ("LobbyScene");
	}

	public void GuiInLobby()
	{
//		if (!displayDebugInformation) {
//			return;
//		}
		string stateLabel = "";
		if (instance.GameInstance != null && instance.GameInstance.NickName != null)
			stateLabel += "name: [" + instance.GameInstance.NickName + "]  ";
		if (!string.IsNullOrEmpty(instance.roomToJoinName))
			stateLabel += "RoomName: [" + instance.roomToJoinName + "]  ";
		if (instance.GameInstance != null && instance.GameInstance.NickName != null)
			stateLabel += "state: [" + instance.GameInstance.State.ToString() + "]  ";
		GUILayout.Label(stateLabel);
		GUILayout.BeginArea(LobbyRect);

		GUILayout.Label("Lobby Screen");
		int playersInRoom = 0;
		int playersLooking4Room = 0;
		int numOfRooms = 0;
		if (instance.GameInstance != null && instance.GameInstance.NickName != null)
			playersInRoom = instance.GameInstance.PlayersInRoomsCount;
		if (instance.GameInstance != null)
			playersLooking4Room = instance.GameInstance.PlayersOnMasterCount;
		if (instance.GameInstance != null && instance.GameInstance.NickName != null)
			numOfRooms = instance.GameInstance.RoomsCount;
		GUILayout.Label(stateLabel);
		GUILayout.Label(string.Format("Players in rooms: {0} looking for rooms: {1}  rooms: {2}", playersInRoom, playersLooking4Room, numOfRooms));
		
		if (GUILayout.Button("Join Random (or create)"))
		{
			joinOrCreateRandomRoom ();
		}

		if (instance.GameInstance != null && instance.GameInstance.SavedGames != null) {
			GUILayout.Label("Saved Games: " + instance.GameInstance.SavedGames.Count);
			foreach (KeyValuePair<string, int> savedRoom in instance.GameInstance.SavedGames)
			{
				string roomName = savedRoom.Key;
				int actorNumber = savedRoom.Value;
				if (GUILayout.Button("ReJoin: " + roomName + " #" + actorNumber))
				{
					instance.GameInstance.OpJoinRoom(roomName, actorNumber);
				}
			}
			
			if (GUILayout.Button("Refresh", GUILayout.Width(150)))
			{
				instance.GetRoomsList();
			}
			GUILayout.Space(20);
			
			GUILayout.Label("Rooms in lobby: " + instance.GameInstance.RoomInfoList.Count);
			foreach (RoomInfo roomInfo in instance.GameInstance.RoomInfoList.Values)
			{
				if (GUILayout.Button(roomInfo.Name + " turn: " + roomInfo.CustomProperties["t#"]))
				{
					instance.GameInstance.OpJoinRoom(roomInfo.Name);
				}
			}
		}
		GUILayout.EndArea();
	}
	
	public void GuiInGame()
	{
		string stateLabel = "";
		if (instance.GameInstance != null && instance.GameInstance.NickName != null)
			stateLabel += "name: [" + instance.GameInstance.NickName + "]  ";
		if (instance.roomToJoinName != null)
			stateLabel += "RoomName: [" + instance.roomToJoinName + "]  ";
		if (instance.GameInstance != null && instance.GameInstance.State != null)
			stateLabel += "state: [" + instance.GameInstance.State.ToString() + "]  ";
		GUILayout.Label(stateLabel);
		leftToolbar = new Rect(leftToolbar.x, leftToolbar.y, leftToolbar.width, Screen.height - leftToolbar.y);
		GUILayout.BeginArea(leftToolbar);
		GUI.skin.button.stretchWidth = false;
		GUI.skin.button.fixedWidth = 150;

		if (instance.GameInstance.CurrentRoom == null) {
			Debug.LogError("Game Instance is null!!!!!?!?!?!?");
		} else {
			// we are in a room, so we can access CurrentRoom and it's Players
			GUILayout.Label("In Room: " + instance.GameInstance.CurrentRoom.Name);

			string interestingPropsAsString = FormatRoomProps();
			if (!string.IsNullOrEmpty(interestingPropsAsString))
			{
				GUILayout.Label("Props: " + interestingPropsAsString);
			}
			
			foreach (Player player in instance.GameInstance.CurrentRoom.Players.Values)
			{
				if (player.ID == instance.GameInstance.lastTurnPlayer)
				{
					GUILayout.Label(player.ToString() + " (played last)");
				}
				else
				{
					GUILayout.Label(player.ToString());
				}
			}
		}

		GUILayout.Space(15);
		
		GUILayout.Label("Save the board by ending the turn.");
		if (GUILayout.Button("End Turn " + instance.GameInstance.turnNumber + " (Save)"))
		{
			instance.GameInstance.SaveBoardAsProperty();
		}
		
		GUILayout.FlexibleSpace();
		
		if (GUILayout.Button("Leave (return later)"))
		{
			saveAndLeaveRoom ();
		}
		if (GUILayout.Button("Abandon"))
		{
			instance.GameInstance.OpLeaveRoom(false);
		}
		GUILayout.EndArea();
	}
	
	public string FormatRoomProps()
	{
		if (instance.GameInstance.CurrentRoom == null) {
			Debug.LogError("GameInstance current room is null");
				return "";
		}
		Hashtable customRoomProps = instance.GameInstance.CurrentRoom.CustomProperties;
		string interestingProps = "";
		foreach (string propName in instance.GameInstance.roomProps)
		{
			if (customRoomProps.ContainsKey(propName))
			{
				if (!string.IsNullOrEmpty(interestingProps)) interestingProps += " ";
				interestingProps += propName + ":" + customRoomProps[propName];
			}
		}
		return interestingProps;
	}
	
	private string RandomCustomRoomProp()
	{
		string[] roomProps = instance.GameInstance.roomProps;
		return roomProps[Random.Range(0, roomProps.Length)];
	}
	
	private string RandomCustomPlayerProp()
	{
		string[] playerProps = instance.GameInstance.playerProps;
		return playerProps[Random.Range(0, playerProps.Length)];
	}
}
