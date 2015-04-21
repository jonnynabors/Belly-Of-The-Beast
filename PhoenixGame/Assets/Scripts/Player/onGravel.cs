using UnityEngine;
using System.Collections;

public class onGravel : MonoBehaviour {
	public playerFootsteps footsteps;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			footsteps.onGravel = true;
		}
	}
	void OnTriggerExit (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			footsteps.onGravel = false;
		}
	}
}
