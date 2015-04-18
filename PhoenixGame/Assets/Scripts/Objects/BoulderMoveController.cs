using UnityEngine;
using System.Collections;
/*
 * Script written by Jonny Nabors, 3/1/2015. Purpose of this script 
 * is to give the boulder free movement when the player comes into 
 * contact with the boulder, or when the puzzle is successfully solved.
 */
public class BoulderMoveController : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag(Tags.player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		//If collision occurs with player
		if(other.gameObject == player)
		{
			//Remove Kinematic property from Rigidbody component.
			GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<Rigidbody>().angularDrag = 10;
		}
	}
}
