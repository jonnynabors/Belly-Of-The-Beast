using UnityEngine;
using System.Collections;

public class PuzzleController : MonoBehaviour {
	
	private GameObject tile1;
	private GameObject tile2;
	private GameObject tile3;

	//references to each tile's script
	private TileController tileScript1;
	public TileController tileScript2;
	private TileController tileScript3;

	//permissions will allow tiles to "stick" to the ground if pressed in proper order
	public bool permission1 = false;
	public bool permission3 = false;

	private GameObject boulder;
	Rigidbody boulderBody;

	void Awake()
	{
		tile1 = GameObject.FindGameObjectWithTag ("Tile1");
		tile2 = GameObject.FindGameObjectWithTag ("Tile2");
		tile3 = GameObject.FindGameObjectWithTag ("Tile3");
		tileScript1 = tile1.GetComponent<TileController> ();
		tileScript2 = tile2.GetComponent<TileController> ();
		tileScript3 = tile3.GetComponent<TileController> ();
		boulder = GameObject.FindGameObjectWithTag ("Boulder");
		boulderBody = boulder.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if all tiles are lowered, release boulder
		if (tileScript1.lowered && tileScript2.lowered && tileScript3.lowered)
			boulderBody.isKinematic = false;
	}
}