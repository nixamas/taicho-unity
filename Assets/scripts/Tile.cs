using UnityEngine;
using System.Collections;
using com.cosmichorizons.enums;
using com.cosmichorizons.basecomponents;

// TODO incorpoate BoardComponent funcionality into object
public class Tile : MonoBehaviour {
	public BoardComponent boardComponent;
	public HighlightTileSprite highlighter;
	public CharacterSprite characterSprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	// TODO Find out if this works for mobile
	void OnMouseDown () {
//		this.hide ();
		TaichoGameGrid taichoGg = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		taichoGg.onTileClicked (this);
	}

	void printImHere () {
		Debug.Log ("I'm Here!!!");
	}

	public void initializeHighlighter (HighlightTileSprite highlighter) {
		this.highlighter = highlighter;
		disableHighlighter ();
	}

	public void highlightSelected () {
		enableHighlighter ();
		if (boardComponent != null && boardComponent.Character.Player == Player.PLAYER_ONE) {
			this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.TILE_SELECT_PLAYERONE;
		} else {
			this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.TILE_SELECT_PLAYERTWO;
		}
	}

	public void highlightMove () {
		enableHighlighter ();
		this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.MOVE_MOVE;
	}

	public void highlightStack () {
		enableHighlighter ();
		if (boardComponent != null && boardComponent.Character.Player == Player.PLAYER_ONE) {
			this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.TILE_STACK_PLAYERONE;
		} else {
			this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.TILE_STACK_PLAYERTWO;
		}
	}

	public void highlightAttack () {
		enableHighlighter ();
		this.highlighter.GetComponent<Renderer> ().material.color = TaichoColors.MOVE_ATTACK;
		this.boardComponent.Attackable = true;
	}

	public void enableHighlighter () {
		this.highlighter.GetComponent<Renderer> ().enabled = true;
		this.boardComponent.Stackable = true;
	}

	public void disableHighlighter () {
		this.highlighter.GetComponent<Renderer> ().enabled = false;
		if (this.boardComponent != null) {
			this.boardComponent.Stackable = false;
			this.boardComponent.Attackable = false;
		}
	}

	public void initializeSprite (CharacterSprite sprite) {
		this.characterSprite = sprite;
		disableSprite ();
	}

	public void disableSprite () {
		this.characterSprite.GetComponent<Renderer> ().enabled = false;
	}

	public void enableSprite () {
		this.characterSprite.GetComponent<Renderer> ().enabled = true;
	}

	public void updateSprite () {
		if (boardComponent != null && !boardComponent.Occupied) {
			disableSprite ();
		} else if (boardComponent != null) {
			if (boardComponent.Character.Rank == Ranks.LEVEL_ONE) {
				this.characterSprite.populateLevelOne ();
			} else if (boardComponent.Character.Rank == Ranks.LEVEL_TWO) {
				this.characterSprite.populateLevelTwo ();
			} else if (boardComponent.Character.Rank == Ranks.LEVEL_THREE) {
				this.characterSprite.populateLevelThree ();
			} else if (boardComponent.Character.Rank == Ranks.TAICHO) {
				this.characterSprite.populateTaicho ();
			}

			this.characterSprite.Color = getRanksColor(boardComponent.Character.Rank, boardComponent.Character.Player);//boardComponent.CharacterPlayer.getPlayerColor();
			enableSprite ();
		} else {
			Debug.LogError("null boardcompontent for tile" + this);
		}
	}

	public void hide () {
		this.GetComponent<Renderer> ().enabled = false;
	}

	public Color getRanksColor (Ranks rank, Player player) {
		Color color = Color.black;
		if (player == Player.PLAYER_ONE) {
			switch (rank) {
			case Ranks.LEVEL_ONE:
				color = TaichoColors.PLAYERONE_LVL1; 
				break;
			case Ranks.LEVEL_TWO:
				color = TaichoColors.PLAYERONE_LVL2; 
				break;
			case Ranks.LEVEL_THREE:
				color = TaichoColors.PLAYERONE_LVL3; 
				break;
			case Ranks.TAICHO:
				color = TaichoColors.PLAYERONE_TAICHO; 
				break;
			}
		} else if (player == Player.PLAYER_TWO) {
			switch (rank) {
			case Ranks.LEVEL_ONE:
				color = TaichoColors.PLAYERTWO_LVL1; 
				break;
			case Ranks.LEVEL_TWO:
				color = TaichoColors.PLAYERTWO_LVL2; 
				break;
			case Ranks.LEVEL_THREE:
				color = TaichoColors.PLAYERTWO_LVL3; 
				break;
			case Ranks.TAICHO:
				color = TaichoColors.PLAYERTWO_TAICHO; 
				break;
			}
		}
		return color;
	}


}
