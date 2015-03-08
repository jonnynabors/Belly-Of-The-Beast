using UnityEngine;
using System.Collections;

public class chatBubble : MonoBehaviour {
	public float range;
	public bool closeEnough;
	public GameObject NPC;
	public Vector3 vec;
	public GameObject Player;

	public GameObject bubble;
	public Transform target;
	public float speed;
	public Canvas chat;

	// Use this for initialization
	void Start () {
		chat = GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		chat.enabled = true;
		rotate ();
		//if player is not in range, deactivate chat bubble
		if (!detectRange())
			chat.enabled = false;
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