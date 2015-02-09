/*using UnityEngine;
using System.Collections;

public class IntroCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	var lookAtTarget : Transform; //What to look at 
	var current : Transform; //Where to start 
	var transitionTime : float = 1.0f; //Time to take to transition
	
	function Start() { //Initial positioning 
		if(current) transform.position = current.position; //position 
		if(lookAtTarget) transform.LookAt(lookAtTarget); //look at target 
	}
		
		//Call this with the transform to move to. 
		static function MoveCamera(target : Transform) { if(!target || current == target) return; //Nowhere to go 
			if(!current) { //Nowhere to start from, so initial positioning 
				transform.position = target.position; //position 
				if(lookAtTarget) transform.LookAt(lookAtTarget); //look at target 
			} 
			else { var control : float = 0; //Amount along transition 
				while(control < 1.0) { //Continue until we reach the destination 
						control += Time.deltaTime/transitionTime; //move along transition 
						transform.position = Vector3.Lerp(current.position, target.position, Mathf.SmoothStep(0.0, 1.0, control)); //Smoothing optional 
						if(lookAtTarget) transform.LookAt(lookAtTarget); //look at target 
				yield; //wait 
			} 
		} 
		current = target; //we're now at the target's position; }
}
*/