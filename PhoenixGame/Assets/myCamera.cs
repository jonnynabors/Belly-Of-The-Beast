using UnityEngine;
using System.Collections;

public class myCamera : MonoBehaviour {

	public float distanceAway = 1f;
	public float distanceUp = 1f;
	private float smooth = 5f;
	public Transform follow;
	public Vector3 targetPosition;
	public Vector3 lookDir;
	//position of camera
	public Vector3 offset = new Vector3(0f, -0.72f, 0f);
	
	private Vector3 velocityCamSmooth = Vector3.zero;
	private float camSmoothDampTime = 0.1f;
	
	// Use this for initialization
	void Start () {
		follow = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();		
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate(){
		
		Vector3 charOffset = follow.position + offset;

		lookDir = charOffset - this.transform.position;
		lookDir.y = 0;
		lookDir.Normalize ();
		Debug.DrawRay (this.transform.position, lookDir, Color.green);
		
		targetPosition = charOffset + follow.up * distanceUp - lookDir * distanceAway;
		//targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
		//transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * smooth);
		//		Debug.DrawRay (follow.position, Vector3.up * distanceUp, Color.red);
		//		Debug.DrawRay (follow.position, 1f * follow.forward * distanceAway, Color.blue);
		Debug.DrawLine (follow.position, targetPosition, Color.magenta);
		
		
		
		
		smoothPosition(this.transform.position, targetPosition);
		
		transform.LookAt (follow);
		
	}
	
	private void smoothPosition(Vector3 fromPos, Vector3 toPos){
		
		this.transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
		
		
	}
	
}