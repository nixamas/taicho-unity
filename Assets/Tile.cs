using UnityEngine;
using System.Collections;
using com.cosmichorizons.enums;
using com.cosmichorizons.basecomponents;

// TODO incorpoate BoardComponent funcionality into object
public class Tile : MonoBehaviour {
	public BoardComponent boardComponent;
	public HighlightTileSprite highlighter;
	public CharacterSprite characterSprite;

//	private Coordinate coordinate;
//	private Location location;
//	private Color color = Color.black;
//	private bool occupied = false;
//	private bool stackable = false;
//	private bool selected = false;
//	private bool attackable = false;
//	private bool barrier, timeServed;
//	private MovableObject character;
//	private bool highlight;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
//		if( Input.GetMouseButtonDown(0) ) {
//			Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
//			RaycastHit hit;
//			Debug.Log( "sdfsdfsdfsdfsdfsdfsdf" );
//			
//			if( Physics.Raycast( ray, out hit, 100 ) )
//			{
//				Debug.Log( hit.transform.gameObject.name );
//			}
//		}
	}

//	public void onTileSelected (Object selectedTile) {
//		Debug.Log( "Tile.onDownEvent - " + selectedTile );
//	}


//	void OnMouseOver () {
//
//	}

	// TODO Find out if this works for mobile
	void OnMouseDown () {
		TaichoGameGrid taichoGg = (TaichoGameGrid) GameObject.FindGameObjectWithTag("TaichoGameGrid").GetComponent<TaichoGameGrid>();
		if (this.boardComponent.Id == 8) {
			//TODO change to use an actual button
			taichoGg.onUnstackButtonClicked();
		} else {
			taichoGg.onTileClicked (this);
		}
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
		this.highlighter.GetComponent<Renderer> ().material.color = Color.black;
	}

	public void highlightMove () {
		enableHighlighter ();
		this.highlighter.GetComponent<Renderer> ().material.color = Color.green;
	}

	public void highlightStack () {
		enableHighlighter ();
		this.highlighter.GetComponent<Renderer> ().material.color = Color.yellow;
	}

	public void highlightAttack () {
		enableHighlighter ();
		this.highlighter.GetComponent<Renderer> ().material.color = Color.red;
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
		}
		return color;
	}


}
