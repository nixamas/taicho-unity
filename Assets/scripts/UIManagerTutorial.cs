using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManagerTutorial : UIManagerGame {
//	public TaichoGameGrid grid;
	public Text headerText, tutorialText;
	public Button backButton, nextButton;
	private static int numberOfPanels = 7;
	private int currentPanelIndex = 0;	//also the starting panel index
	private AbstractPanel[] panels = new AbstractPanel[ numberOfPanels ];

	// Use this for initialization
	void Start () {
		panels [0] = new Panel01 ();
		panels [1] = new Panel02 ();
		panels [2] = new Panel03 ();
		panels [3] = new Panel04 ();
		panels [4] = new Panel05 ();
		panels [5] = new Panel06 ();
		panels [6] = new Panel07 ();

		loadPanel (panels [currentPanelIndex]);
//		GameObject.FindGameObjectWithTag ("GameMenuButton").GetComponent<Button> ().image.enabled = false;
//		GameObject.FindGameObjectWithTag ("GameMenuButton").GetComponent<Button> ().GetComponentInChildren<Text> ().enabled = false;
//		hideMenu ();
	}
	
	// Update is called once per frame
	void Update () {
		if (this.grid == null) {
			this.grid = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		}
		updateUnstackButton ();
	}

	// public void StartGame() in base class

	public void nextPanel () {
		if (currentPanelIndex + 1 == numberOfPanels) {
			return;
		}
		AbstractPanel panel = panels [++currentPanelIndex];
		loadPanel (panel);
	}

	public void previousPanel () {
		if (currentPanelIndex - 1 < 0) {
			return;
		}
		AbstractPanel panel = panels [--currentPanelIndex];
		loadPanel (panel);
	}

	private void loadPanel(AbstractPanel panel) {
		this.headerText.text = panel.getHeaderText();
		this.tutorialText.text = panel.getTutorialText();
		updateButtons ();
		//panel.processGameGrid ((TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>());
		panel.processGameGrid (this.grid);
	}

	private void updateButtons () {
		if (currentPanelIndex == 0) {
			this.backButton.GetComponent<Button> ().interactable = false;
			this.nextButton.GetComponent<Button> ().interactable = true;
		} else if (currentPanelIndex == (numberOfPanels - 1)) {
			this.backButton.GetComponent<Button> ().interactable = true;
			this.nextButton.GetComponent<Button> ().interactable = false;
		} else {
			this.backButton.GetComponent<Button> ().interactable = true;
			this.nextButton.GetComponent<Button> ().interactable = true;
		}
	}
}
