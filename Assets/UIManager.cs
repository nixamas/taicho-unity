using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
