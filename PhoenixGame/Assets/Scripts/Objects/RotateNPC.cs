using UnityEngine;
using System.Collections;

public class RotateNPC : MonoBehaviour {
	public float range;
	public bool closeEnough;
	public GameObject NPC;
	public Vector3 vec;
	public GameObject Player;
	public Transform target;
	public float speed;

	// Update is called once per frame
	void Update () {
		if (detectRange ())
			rotate ();
	}

	// Use this for initialization
	void Awake () {
		Player = GameObject.FindGameObjectWithTag(Tags.player);
	}

	bool detectRange(){
		//function to detect if the player is close enough to the NPC
		//will return false if they are not within the specified range
		closeEnough = false;
		vec = GetComponentInParent<Rigidbody> ().position;
		//if player is within range
		if (Vector3.Distance(vec, Player.transform.position) <= range)
			closeEnough = true;
		
		return closeEnough;
	}

	void rotate(){
		//function will rotate one object's rotation transform to face another object
		Vector3 targetDir = target.position - transform.position;
		float step = speed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		Debug.DrawRay(transform.position, newDir, Color.red);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
}
