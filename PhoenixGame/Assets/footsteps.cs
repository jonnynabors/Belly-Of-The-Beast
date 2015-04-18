using UnityEngine;
using System.Collections;

public class footsteps : MonoBehaviour {

	public CapsuleCollider cc;
	public bool grounded;
	public float rVelocity;
	public AudioClip[] walk, run, sprint, water, current;



	// Use this for initialization
	void Start () {
		cc = GetComponent<CapsuleCollider> ();
	}

	// Update is called once per frame
	void Update () {
		//grounded = isGrounded ();
		rVelocity = GetComponent<Rigidbody>().velocity.magnitude;
		GetComponent<AudioSource>().clip = current[Random.Range (0, current.Length)];
		if(grounded == true && rVelocity > .2f){
			GetComponent<AudioSource>().volume = Random.Range (0.8f, 1);
			GetComponent<AudioSource>().pitch = Random.Range (0.8f, 1.1f);
			GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
		}
	}
	bool isGrounded(){
		bool isGrounded;
		isGrounded = Physics.CheckCapsule(cc.bounds.center,new Vector3(cc.bounds.center.x,cc.bounds.min.y-0.01f,cc.bounds.center.z),0.018f);


		return isGrounded;
	}
	void OnCollisionEnter(Collision other){
		if(other.gameObject.tag == "Grass" || other.gameObject.tag == "Water")
		{
			grounded = true;
		}
		if (other.gameObject.tag == "Grass") {
			current = run;
				}
		if (other.gameObject.tag == "Water") {
			current = water;
		}
	}
	void OnCollisionExit(Collision other){
		if(other.gameObject.tag == "Grass" || other.gameObject.tag == "Water")
		{
			grounded = false;
		}
	}
}
