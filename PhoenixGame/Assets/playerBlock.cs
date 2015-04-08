using UnityEngine;
using System.Collections;

public class playerBlock : MonoBehaviour {

	public Animator anim;
	
	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	
	
	
	void Update ()
	{		
		if (Input.GetMouseButton (1)) {
			block ();
		}
		else{
			anim.SetBool ("isBlocking", false);
		}
		
	}
	
	
	public void DisableEffects ()
	{
		
	}
	
	
	void block ()
	{
		anim.SetBool ("isBlocking", true);
	}
}