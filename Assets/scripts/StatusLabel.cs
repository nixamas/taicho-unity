using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusLabel : MonoBehaviour {
	public enum states { CONNECTED, CONNECTING, DISCONNECTED }
	public Color32 disconnectedColor, connectingColor, connectedColor;
	public Image labelStatusImage;
	public Text userNameTextOutput;
	public string networkUserName;

	public states state;
	// Use this for initialization
	void Start () {
	
	}

	//		JoinedLobby
	//		ConnectedToMaster
	//		ConnectingToMasterserver
	//		Authenticating
	//		ConnectingToNameServer
	// Update is called once per frame
	void Update () {
		if (NetworkManager.instance.GameInstance != null && NetworkManager.instance.GameInstance.State != null) {
			string statusString = NetworkManager.instance.GameInstance.State.ToString ();
			if (statusString == "ConnectingToNameServer"
			    || statusString == "Authenticating"
			    || statusString == "ConnectingToMasterserver") {
				state = states.CONNECTING;
			} else if (statusString == "JoinedLobby") {
				state = states.CONNECTED;
			} else {
				state = states.DISCONNECTED;
			}
		} else {
			state = states.DISCONNECTED;
		}

		Color32 labelColor = disconnectedColor;
		switch(state)
		{
		case states.CONNECTED: 
			labelColor = connectedColor;
			break;
		case states.CONNECTING:
			labelColor = connectingColor;
			break;
		case states.DISCONNECTED:
			break;
		}
		
		labelStatusImage.color = labelColor;

		userNameTextOutput.text = "  " + networkUserName;
	}

	public void OnGUI () {

	}
}
