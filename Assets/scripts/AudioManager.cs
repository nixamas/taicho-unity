using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	//fix all of this


	public AudioClip slideAudio;
	public AudioClip tileSelectAudio;
	public AudioClip characterDestroyedAudio;
	public AudioSource backgroundMusicSource;
	public bool playBackgroundAudio = false;
	public virtual bool BackgroundAudio {
		set {
			this.playBackgroundAudio = value;
			if (this.playBackgroundAudio) {
				this.backgroundMusicSource.enabled = true;
				this.backgroundMusicSource.Play ();
			} else {
				this.backgroundMusicSource.Pause ();
			}
		}
		get {
			return this.playBackgroundAudio;
		}
	}

	//TODO may play these sounds at the tile vector point
	public void playTileSlideSound () {
		if (this.playBackgroundAudio) AudioSource.PlayClipAtPoint(this.slideAudio, new Vector3(transform.position.x, transform.position.y, transform.position.z));
	}

	public void playTileSelectedSound () {
		if (this.playBackgroundAudio) AudioSource.PlayClipAtPoint(this.tileSelectAudio, new Vector3(transform.position.x, transform.position.y, transform.position.z));
	}

	public void playCharacterDestroyedSound () {
		if (this.playBackgroundAudio) AudioSource.PlayClipAtPoint(this.characterDestroyedAudio, new Vector3(transform.position.x, transform.position.y, transform.position.z));
	}

	// Use this for initialization
	void Start () {
		this.backgroundMusicSource.Play ();
		this.backgroundMusicSource.Pause ();
		this.BackgroundAudio = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
