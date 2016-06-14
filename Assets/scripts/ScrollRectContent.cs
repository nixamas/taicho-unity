using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScrollRectContent : MonoBehaviour {
	public GameObject content;
	public Button button;
	
	// Use this for initialization
	void Start () {
		content.GetComponent<Transform> ().DetachChildren ();

//		Transform contentTransform = content.GetComponent<Transform> ();
//		for (int i = 0; i < 6; i++) {
//			Button b = (Button) GameObject.Instantiate(button, button.GetComponent<Transform> ().position, button.GetComponent<Transform> ().rotation);
//			b.GetComponentInChildren<Text> ().text = "Saved Game " + i;
//			b.GetComponent<Transform> ().SetParent(contentTransform);
//		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void clearList () {
		foreach (Transform child in content.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}
	
	public void addItemToList (string roomName, string actorId) {
		Transform contentTransform = content.GetComponent<Transform> ();
		Button b = (Button) GameObject.Instantiate(button, contentTransform.position, contentTransform.rotation);
		b.GetComponent<LobbyScrollButton> ().roomName = roomName;
		b.GetComponent<LobbyScrollButton> ().actorTurn = actorId;
		b.GetComponentInChildren<Text> ().text = "REJOIN :: " + roomName + "["+actorId+"]";
		b.GetComponent<Transform> ().SetParent(contentTransform);
	}
}

