using UnityEngine;
using System.Collections;

public class newCamera : MonoBehaviour {

	public float maxDistance = 1;
	public float viewAngle = 1;
	public float distanceUp = 5f;
	public float distanceAway = 1f;
	public Transform follow;
	public Vector3 targetPosition;
	public Vector3 lookDir;
	public float rotationUpdateSpeed = 60.0f,
	lookUpSpeed = 20.0f;
	public Camera camera;
	
	public LayerMask obstacleLayers = -1, groundLayers = -1;
	
	//position of camera
	public Vector3 offset = new Vector3(0f, 0.0f, 0f);
	
	private Vector3 velocityCamSmooth = Vector3.zero;
	private float camSmoothDampTime = 0.1f;
	public bool grounded = false;
	public float groundedDistance = 0.5f;
	public float groundedCheckOffset = 0.0f;
	public bool lookFromBelow = false;
	public Vector3 prevPos = new Vector3 (0f,0f,0f);
	float ViewRadius
		// The minimum clear radius between the camera and the target
	{
		get
		{
			float fieldOfViewRadius = (distanceAway / Mathf.Sin (90.0f - camera.fieldOfView / 2.0f)) * Mathf.Sin (camera.fieldOfView / 2.0f);
			// Half the width of the field of view of the camera at the position of the target
			float doubleCharacterRadius = Mathf.Max (follow.gameObject.GetComponent<Collider>().bounds.extents.x, follow.gameObject.GetComponent<Collider>().bounds.extents.z) * 2.0f;
			
			return Mathf.Min (doubleCharacterRadius, fieldOfViewRadius);
		}
	}
	
	
	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		follow = GameObject.FindGameObjectWithTag(Tags.player).transform;
	}
	
	void FixedUpdate ()
	{
		grounded = Physics.Raycast (
			camera.transform.position + follow.up * -groundedCheckOffset,
			follow.up * -1,
			groundedDistance,
			groundLayers
			);
		Vector3 inverseLineOfSight = camera.transform.position - follow.position;
		RaycastHit hit;
		if (Physics.SphereCast (follow.position, ViewRadius, inverseLineOfSight, out hit, distanceAway, obstacleLayers))
			// Cast a sphere from the target towards the camera - using the view radius - checking against the obstacle layers
		{
			distanceAway = Mathf.Min ((hit.point - follow.position).magnitude, distanceAway);
			// If something is hit, set the target distance to the hit position
		}
		else
		{
			distanceAway = maxDistance;
			// If nothing is hit, target the optimal distance
		}
	}
	void Update () {
	}
	
	void LateUpdate(){
		
		float rotationAmount;
		
		rotationAmount = Input.GetAxis ("Mouse X") * rotationUpdateSpeed * Time.deltaTime;
		camera.transform.RotateAround (follow.position, Vector3.up, rotationAmount);
		rotationAmount = Input.GetAxis ("Mouse Y") * -1.0f * lookUpSpeed * Time.deltaTime;
		camera.transform.RotateAround (follow.position, camera.transform.right, rotationAmount);
		
		DistanceUpdate ();
		followUpdate ();
	}
	
	void followUpdate(){
		Vector3 charOffset = follow.position + offset;
		
		lookDir = charOffset - this.transform.position;
		lookDir.Normalize ();
		Debug.DrawRay (this.transform.position, lookDir, Color.green);
		
		targetPosition = charOffset + follow.up * distanceUp - lookDir * distanceAway;
		Debug.DrawLine (follow.position, targetPosition, Color.magenta);
		
		//wallCollision (charOffset, ref targetPosition);
		smoothPosition(this.transform.position, targetPosition);
		transform.LookAt (new Vector3 (follow.position.x, follow.position.y + viewAngle, follow.position.z));
	}
	void DistanceUpdate(){
		targetPosition = follow.position + (camera.transform.position - follow.position).normalized * distanceAway;
	}
	
	private void smoothPosition(Vector3 fromPos, Vector3 toPos){
		lookFromBelow = Vector3.Angle (camera.transform.forward, follow.up * -1) > Vector3.Angle (camera.transform.forward, follow.up);
		if (grounded && lookFromBelow) {
			this.transform.position = Vector3.SmoothDamp (fromPos, new Vector3 (toPos.x, follow.position.y + viewAngle, toPos.z), ref velocityCamSmooth, camSmoothDampTime);
			//this.transform.position = new Vector3 (toPos.x,follow.transform.position.y,toPos.z);
			if (toPos.y > camera.transform.position.y) {
				lookFromBelow = false;
			}
		} else {
			this.transform.position = Vector3.SmoothDamp (fromPos, new Vector3 (toPos.x, camera.transform.position.y + (follow.position.y - prevPos.y)*3, toPos.z), ref velocityCamSmooth, camSmoothDampTime);
			prevPos = follow.position;
		}
		
	}
	private void wallCollision(Vector3 fromObject, ref Vector3 toTarget){
		Debug.DrawLine (fromObject, toTarget, Color.cyan);
		RaycastHit wallHit = new RaycastHit ();
		if(Physics.Linecast(fromObject, toTarget, out wallHit)){
			toTarget = new Vector3(toTarget.x, toTarget.y, wallHit.point.z);
		}
	}
	void OnDrawGizmosSelected(){
		Gizmos.color = grounded ? Color.blue : Color.red;
		Gizmos.DrawLine (camera.transform.position + follow.up * -groundedCheckOffset,
		                 camera.transform.position + follow.up * -(groundedCheckOffset + groundedDistance));
	}
	
}
