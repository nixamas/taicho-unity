using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	public NetworkManager networkManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void StartGame() {
		Application.LoadLevel ("GameBoardScene");
	}


	public Color getColorSelected(Color color) {
		return new Color(color.r, color.g, color.b);
	}
}
