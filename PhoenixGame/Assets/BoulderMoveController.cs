using UnityEngine;
using System.Collections;
/*
 * Script written by Jonny Nabors, 3/1/2015. Purpose of this script 
 * is to give the boulder free movement when the player comes into 
 * contact with the boulder, or when the puzzle is successfully solved.
 */
public class BoulderMoveController : MonoBehaviour {
	private GameObject player;
	private GameObject boulder;
	// Use this for initialization
	void Awake () {
		player = GameObject.FindGameObjectWithTag(Tags.player);
		boulder = GameObject.FindGameObjectWithTag(Tags.boulder);
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
			rigidbody.isKinematic = false;
			rigidbody.angularDrag = 10;
		}
	}
}
