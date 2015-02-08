using UnityEngine;
using System.Collections;

public class myThirdPersonCamera : MonoBehaviour {

	[SerializeField]
	private float distanceAway;
	[SerializeField]
	private float distawnceUp;
	[SerializeField]
	private float smooth;
	[SerializeField]
	private Transform follow;
	private Vector3 targetPosition;

	// Use this for initialization
	void Start () {
		follow = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void LateUpdate()
	{
		//setting the target position to be the correct offset from the player
		targetPosition = follow.position + follow.up * distawnceUp - follow.forward * distanceAway;
		//making a smooth transition between cameras current position and the position it wants to be in
		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		//make sure the camera is looking the right way
		transform.LookAt (follow);
	}
}
