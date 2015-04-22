using UnityEngine;
using System.Collections;
using com.cosmichorizons.enums;
using com.cosmichorizons.basecomponents;

// TODO incorpoate BoardComponent funcionality into object
public class Tile : MonoBehaviour {
	public BoardComponent boardComponent;
	public HighlightTileSprite highlighter;
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
		Debug.Log (" taichogg ["+ taichoGg +"]");
		if (this.boardComponent.Id == 1) {
			//TODO change to use an actual button
			taichoGg.onUnstackButtonClicked();
		} else {
			taichoGg.onTileClicked (this);
		}

//		Debug.Log ("You clicked a tile :: " + gameObject);
//		//may not need to do this     gameObject.GetComponent<Tile>();
//		Tile tile = gameObject.GetComponent<Tile>();
//		Debug.Log (" Board Component clicked ["+ boardComponent +"]");
//		if (boardComponent.Occupied) {
//			tile.printImHere ();
//			Debug.Log ("BC :: " + boardComponent);
//			tile.GetComponent<Renderer> ().material.color = Color.green;
//
//
//		}

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
}
