using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerMenu : UIManager {

	public Image logoSwordImage, logoBackground, logoForeground;
	public Button [] menuButtons;

	private int timeIndex = 0, delay = 4, posDelta = 7;

	// Use this for initialization
	void Start () {
		if (logoBackground != null) {
			logoBackground.GetComponent<Image> ().color = TaichoColors.TAICHO_LOGO_TEXT;
		}
		if (logoForeground != null) {
			logoForeground.GetComponent<Image> ().color = TaichoColors.TAICHO_LOGO_TEXT;
		}
		foreach (Button button in menuButtons) {
			button.GetComponent<Image> ().color = TaichoColors.TAICHO_MENU_BUTTONS;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (timeIndex++ == delay && logoSwordImage.GetComponent<RectTransform> ().position.x < 455) {
			Vector3 v = logoSwordImage.GetComponent<RectTransform> ().position;
			logoSwordImage.GetComponent<Transform> ().position = new Vector3(v.x + posDelta, v.y, v.z);
			timeIndex = 0;
		}
	}

	// public void StartGame() in base class

	public void LoadTutorial () {
		Debug.Log ("TODO make tutorial");
		Application.LoadLevel ("TutorialScene");
	}

	public void LoadSettings () {
		Debug.Log ("TODO make settings page");
	}
}
