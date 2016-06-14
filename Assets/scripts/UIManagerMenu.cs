using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerMenu : UIManager {

	public Image logoSwordImage, logoBackground, logoForeground;
	public Button [] menuButtons;
	public Text userNameInputText;
	public Color menuButtonColor, logoBackgroundColor, logoForegroundColor;//, sceneBackgroundTop, sceneBackgroundBottom;

	private int timeIndex = 0, delay = 4, posDelta = 7;

	// Use this for initialization
	void Start () {
		if (logoBackground != null) {
			logoBackground.GetComponent<Image> ().color = getColorSelected(logoBackgroundColor);;
		}
		if (logoForeground != null) {
			Debug.Log("stmt 1 ["+logoForegroundColor+"]");
			logoForeground.GetComponent<Image> ().color = getColorSelected(logoForegroundColor);
		}
		foreach (Button button in menuButtons) {
			button.GetComponent<Image> ().color = getColorSelected(menuButtonColor);
		}
	}

	// Update is called once per frame
	void Update () {
		// TODO re-implement the sword transition
//		if (timeIndex++ == delay && logoSwordImage.GetComponent<RectTransform> ().position.x < 455) {
//			Vector3 v = logoSwordImage.GetComponent<RectTransform> ().position;
//			logoSwordImage.GetComponent<Transform> ().position = new Vector3(v.x + posDelta, v.y, v.z);
//			timeIndex = 0;
//		}

		if (logoBackground != null) {
			logoBackground.GetComponent<Image> ().color = getColorSelected(logoBackgroundColor);
		}
		if (logoForeground != null) {
			logoForeground.GetComponent<Image> ().color = getColorSelected(logoForegroundColor);
		}
		foreach (Button button in menuButtons) {
			button.GetComponent<Image> ().color = getColorSelected(menuButtonColor);
		}
	}

	// public void StartGame() in base class

	public void LoadTutorial () {
		Application.LoadLevel ("TutorialScene");
	}

	public void LoadSettings () {
		Debug.Log ("TODO make settings page");
	}

	public void startOnlineTurnbasedGame () {
		NetworkManager.instance.joinLobbyRoom (userNameInputText.text);
		if (NetworkManager.instance.myUserName == null || NetworkManager.instance.myUserName == "") {
			// TODO Set default user in different way
			NetworkManager.instance.myUserName = "obi1KaNoB-6601";
		}
		Debug.Log ("Attempting to join online lobby as ["+NetworkManager.instance.myUserName+"]");
		Application.LoadLevel ("LobbyScene");
	}
}
