using UnityEngine;
using System.Collections;

public class CharacterSprite : MonoBehaviour {
	public Sprite levelOneSprite;
	public Sprite levelTwoSprite;
	public Sprite levelThreeSprite;
	public Sprite taichoSprite;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void populateLevelOne () {
		this.GetComponent<SpriteRenderer> ().sprite = levelOneSprite;
	}

	public void populateLevelTwo () {
		this.GetComponent<SpriteRenderer> ().sprite = levelTwoSprite;
	}

	public void populateLevelThree () {
		this.GetComponent<SpriteRenderer> ().sprite = levelThreeSprite;
	}

	public void populateTaicho () {
		this.GetComponent<SpriteRenderer> ().sprite = taichoSprite;
	}

	public virtual Color Color {
		set {
			this.GetComponent<SpriteRenderer> ().material.color = value;
		}
	}

}
