using UnityEngine;
using System.Collections;

public class TileController : MonoBehaviour {

	public GameObject tile;
	public bool lowered;
	
	//script reference
	public PuzzleController puzzleController;
	public GameObject puzzle;

	void Start()
	{
		puzzle = GameObject.FindGameObjectWithTag ("Puzzle");
		puzzleController = puzzle.GetComponent<PuzzleController> ();
	}

	void OnTriggerEnter(Collider other)
	{	
		if (other.gameObject.CompareTag ("Player")) 
		{	//if tile is raised...
			if(!lowered){
				tile.transform.localScale += new Vector3 (0f, -0.009f, 0f);	//raises the tile
				lowered = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{	//make sure collision is with the 'Player'
		if (other.gameObject.CompareTag("Player"))
		{	//Instructions for 'Tile2'
			if (tile.CompareTag("Tile2"))
			{
				puzzleController.permission1 = true;
			}

			//Instructions for 'Tile1'
			//if it has permission to stick..
			if (tile.CompareTag("Tile1") && puzzleController.permission1)
			{
				puzzleController.permission3 = true;
			}
			//does not have permission.. raise platform and reset lowered flag
			if (tile.CompareTag("Tile1") && !puzzleController.permission1)
			{
				tile.transform.localScale += new Vector3 (0f, 0.009f, 0f);	//raises the tile
				lowered = false;
			}

			//Instructions for Tile3
			//if it has permission to stick.. do nothing
			if (tile.CompareTag("Tile3") && puzzleController.permission3);
			//does not have permission.. raise platform and reset lowered flag
			if (tile.CompareTag("Tile3") && !puzzleController.permission3)
			{
				tile.transform.localScale += new Vector3 (0f, 0.009f, 0f);	//raises the tile
				lowered = false;
			}
		}
	}
}