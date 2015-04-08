using UnityEngine;
using System.Collections;

public class newCamera : MonoBehaviour {
	
	public float distanceAway = 5f;
	private float distanceUp = 5f;
	private float smooth = 5f;
	public Transform follow;
	public Vector3 targetPosition;
	public Vector3 lookDir;
	public float rotationUpdateSpeed = 60.0f,
	lookUpSpeed = 20.0f;
	
	//position of camera
	public Vector3 offset = new Vector3(0f, 0.0f, 0f);
	
	private Vector3 velocityCamSmooth = Vector3.zero;
	private float camSmoothDampTime = 0.1f;
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		follow = GameObject.Find ("Player").GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void LateUpdate(){
		
		float rotationAmount;
		
		rotationAmount = Input.GetAxis ("Mouse X") * rotationUpdateSpeed * Time.deltaTime;
		camera.transform.RotateAround (follow.position, Vector3.up, rotationAmount);
		rotationAmount = Input.GetAxis ("Mouse Y") * -1.0f * lookUpSpeed * Time.deltaTime;
		camera.transform.RotateAround (follow.position, camera.transform.right, rotationAmount);
		
		followUpdate ();
	}
	
	void followUpdate(){
		Vector3 charOffset = follow.position + offset;
		
		lookDir = charOffset - this.transform.position;
		lookDir.Normalize ();
		Debug.DrawRay (this.transform.position, lookDir, Color.green);
		
		targetPosition = charOffset + follow.up * distanceUp - lookDir * distanceAway;
		Debug.DrawLine (follow.position, targetPosition, Color.magenta);

		smoothPosition(this.transform.position, targetPosition);
		
		transform.LookAt (follow);
	}
	
	private void smoothPosition(Vector3 fromPos, Vector3 toPos){
		if (camera.transform.position.y > 2.9) {
			this.transform.position = new Vector3 (toPos.x, 2.9f, toPos.z);
		} 
		else if(camera.transform.position.y < 1.5){
			this.transform.position = new Vector3 (toPos.x, 1.5f, toPos.z);
		}
		else {
			this.transform.position = new Vector3 (toPos.x,camera.transform.position.y,toPos.z);
		}
		
		//this.transform.position = new Vector3 (toPos.x,camera.transform.position.y,toPos.z);
	}
	
}
