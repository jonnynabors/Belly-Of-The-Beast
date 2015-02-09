using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	Animator anim;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
		Animating (h,v);
	}

	void Animating (float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}
}
