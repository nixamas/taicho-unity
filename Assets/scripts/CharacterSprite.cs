using UnityEngine;
using System.Collections;

public class CharacterSprite : MonoBehaviour {
	string levelone = "Assets/Resources/Images/levelone.png";
	string leveltwo = "Assets/Resources/Images/leveltwo.png";
	string levelthree = "Assets/Resources/Images/levelthree.png";
	string taicho = "Assets/Resources/Images/taicho.png";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void populateLevelOne () {
		this.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.LoadAssetAtPath (levelone, typeof(Sprite));
	}

	public void populateLevelTwo () {
		this.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.LoadAssetAtPath (leveltwo, typeof(Sprite));
	}

	public void populateLevelThree () {
		this.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.LoadAssetAtPath (levelthree, typeof(Sprite));
	}

	public void populateTaicho () {
		this.GetComponent<SpriteRenderer> ().sprite = (Sprite)Resources.LoadAssetAtPath (taicho, typeof(Sprite));
	}

	public virtual Color Color {
		set {
			this.GetComponent<SpriteRenderer> ().material.color = value;
		}
	}

}
