using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Domino : MonoBehaviour {

	public Sprite suiteSprite;
	public Sprite weightSprite;

	// Use this for initialization
	void Start () {
		Sprite ss = this.GetComponentInChildren<Sprite> ();
		Debug.Log ("ss :: " + ss);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		Debug.Log ("onmousedown");
	}
}
