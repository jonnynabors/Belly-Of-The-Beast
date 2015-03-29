using UnityEngine;
using System.Collections;

public class myControllerAnim : MonoBehaviour {

	public Animator animator;
	private Transform transform;
	public myCamera mainCam;
	private float speed = 2.0f;
	public float direction = 0.0f;
	private float angle = 0.0f;
	public float moveX;
	public float moveY;
	public float angleRootToMove;
	
	
	public float s;
	private float d;
	
	private float time = 0f;
	
	private bool isWalking = false;
	private bool isRunning = false;
	private bool isSprinting = false;
	private bool isMoving = false;
	private bool isPivot = false;
	
	public float speedDampTime = 0.1f; 
	public float directionDampTime = 0.1f;
	public float directionSpeed = 3.0f;
	
	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		animator = GetComponent<Animator> ();
		
		if (animator.layerCount >= 2) {
			animator.SetLayerWeight (1, 1);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		//change direction
		Vector3 eulerAngles = transform.eulerAngles;
		//eulerAngles.y = 90;
		//eulerAngles.y = direction;
		//transform.eulerAngles = eulerAngles;
		
		
		moveX = Input.GetAxis ("Horizontal");
		moveY = Input.GetAxis ("Vertical");
		
		s = new Vector2 (moveX, moveY).sqrMagnitude;
		
		
		StickToWorldspace (this.transform, mainCam.transform, ref direction, ref speed);
		animator.SetFloat ("s", setSpeed(speed));
		animator.SetFloat ("Direction", direction, directionDampTime, Time.deltaTime);
		
	}
	
	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut){
		Vector3 rootDirection = root.forward;
		Vector3 stickDirection = new Vector3 (moveX, 0, moveY);
		
		speedOut = stickDirection.sqrMagnitude;
		
		Vector3 CameraDirection = camera.forward;
		CameraDirection.y = 0.0f;
		Quaternion referentialShift = Quaternion.FromToRotation (Vector3.forward, CameraDirection);
		
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross (moveDirection, rootDirection);
		
		Debug.DrawRay (new Vector3 (root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
		Debug.DrawRay (new Vector3 (root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.red);
		Debug.DrawRay (new Vector3 (root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);
		
		
		
		angleRootToMove = Vector3.Angle (rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
		
		angleRootToMove /= 180f;
		
		directionOut = angleRootToMove * directionSpeed;
		
	}
	private float setSpeed(float speed){
		if (Input.GetButton ("Walk")) {
			speed *= 1;
			return speed;
		} 
		else if(Input.GetButton ("Sprint")){
			speed *= 3;
			return speed;
		}
		else{
			speed *= 2;
			return speed;
		}
	}
}
