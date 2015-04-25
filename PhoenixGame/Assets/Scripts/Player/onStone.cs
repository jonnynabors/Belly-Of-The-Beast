using UnityEngine;
using System.Collections;

public class onStone : MonoBehaviour {
	public playerFootsteps playerFootsteps;
	
	
	// Use this for initialization
	void Start () {
		playerFootsteps = GameObject.FindGameObjectWithTag("Player").GetComponent<playerFootsteps>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			playerFootsteps.onStone = true;
		}
	}
	void OnTriggerExit (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			playerFootsteps.onStone = false;
		}
	}
}
