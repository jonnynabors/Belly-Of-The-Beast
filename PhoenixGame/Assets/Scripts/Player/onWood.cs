using UnityEngine;
using System.Collections;

public class onWood : MonoBehaviour {
	public playerFootsteps playerFootsteps;
	
	
	// Use this for initialization
	void Start () {
		playerFootsteps = GameObject.FindGameObjectWithTag("Player").GetComponent<playerFootsteps>();
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			playerFootsteps.onWood = true;
		}
	}
	void OnTriggerExit (Collider collider) {
		if(collider.gameObject.tag == "Player"){
			playerFootsteps.onWood = false;
		}
	}
}
