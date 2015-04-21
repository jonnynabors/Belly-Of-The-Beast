/// <summary>
/// UnityTutorials - A Unity Game Design Prototyping Sandbox
/// <copyright>(c) John McElmurray and Julian Adams 2013</copyright>
/// 
/// UnityTutorials homepage: https://github.com/jm991/UnityTutorials
/// 
/// This software is provided 'as-is', without any express or implied
/// warranty.  In no event will the authors be held liable for any damages
/// arising from the use of this software.
///
/// Permission is granted to anyone to use this software for any purpose,
/// and to alter it and redistribute it freely, subject to the following restrictions:
///
/// 1. The origin of this software must not be misrepresented; you must not
/// claim that you wrote the original software. If you use this software
/// in a product, an acknowledgment in the product documentation would be
/// appreciated but is not required.
/// 2. Altered source versions must be plainly marked as such, and must not be
/// misrepresented as being the original software.
/// 3. This notice may not be removed or altered from any source distribution.
/// </summary>

using UnityEngine;
using System.Collections;

/// <summary>
/// #DESCRIPTION OF CLASS#
/// </summary>
public class myControllerAnim: MonoBehaviour 
{
	
	// Inspector serialized
	[SerializeField]
	private Animator animator;
	[SerializeField]
	private newCamera gamecam;
	[SerializeField]
	private float rotationDegreePerSecond = 120f;
	[SerializeField]
	private float directionSpeed = 1.5f;
	[SerializeField]
	private float directionDampTime = 0.25f;
	[SerializeField]
	private float speedDampTime = 0.05f;
	[SerializeField]
	private float fovDampTime = 3f;
	[SerializeField]
	public float jumpMultiplier;
	[SerializeField]
	private CapsuleCollider capCollider;
	[SerializeField]
	private float jumpDist = 1f;
	
	
	// Private global only
	private float leftX = 0f;
	private float leftY = 0f;
	private AnimatorStateInfo stateInfo;
	private AnimatorTransitionInfo transInfo;
	private float speed = 0f;
	private float direction = 0f;
	private float charAngle = 0f;
	private const float SPRINT_SPEED = 2.0f;	
	private const float SPRINT_FOV = 75.0f;
	private const float NORMAL_FOV = 60.0f;
	private float capsuleHeight;


	//Declare the stamina and player script/objects
	public PlayerStamina playerStamina;
	public GameObject player;
	public Rigidbody rigid;
	public float distanceToTheGround;
	public bool isInTheAir;
	public float fallTime = 0f;
	public float fallDamage = 0f;
	public RaycastHit castToGround;
	public bool isGrounded = false;
	public PlayerHealth playerHealth;


	//if true, the player has restricted movements because of having 0 stamina
	public bool exhausted = false;

	
	public Animator Animator
	{
		get
		{
			return this.animator;
		}
	}
	
	public float Speed
	{
		get
		{
			return this.speed;
		}
	}

	void Start() 
	{
		playerStamina = GetComponent<PlayerStamina> ();
		playerHealth = GetComponent<PlayerHealth> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		rigid = player.GetComponent<Rigidbody> ();
		distanceToTheGround = player.GetComponent<CapsuleCollider> ().bounds.extents.y;
	}

	void Update() 
	{
		exhausted = playerStamina.exhausted;

		//Check if player is on the ground.
//		isInTheAir = IsInTheAir ();

		//Calculate if player is currently falling.
		if (rigid.velocity.y < -2) {
			fallTime += Time.deltaTime;
			animator.SetBool("isFalling", true);
			animator.applyRootMotion = false;
		} else if (rigid.velocity.y > -1 && fallTime > 0) {
			fallDamage = fallTime - Mathf.Floor (fallTime);
			playerHealth.TakeDamage((int)(fallDamage * 150));
			Debug.Log (fallDamage * 150);
			Debug.Log ("Landed");
			fallTime = 0;
			animator.SetBool("isFalling", false);
			animator.applyRootMotion = true;
		} else {
			fallDamage = 0;
		}

//		if (Physics.Raycast (transform.position, -Vector3.up, out castToGround, 100.0f)) {
//			distanceToTheGround = castToGround.distance;
//
//			Debug.Log ("Ray: Hit.Distance = " + castToGround.distance);
//		}

//		if (isInTheAir) {
//			fallTime += Time.deltaTime;
//			if (fallTime > 1.45)
//			{
//				Debug.Log ("I am in the air");
//				animator.SetBool ("isFalling",true);
//			}
//		} else {
//			fallTime = 0;
//			animator.SetBool ("isFalling",false);
//		}

		if (animator) {	
			
			// Pull values from controller/keyboard
			leftX = Input.GetAxis ("Horizontal");
			leftY = Input.GetAxis ("Vertical");	

			if (!exhausted && playerStamina.currentStamina >= 15){
				if (Input.GetButtonDown ("dashRight")) {
					animator.SetTrigger("dashRight");
				}
				if (Input.GetButtonDown ("dashLeft")) {
					animator.SetTrigger("dashLeft");
				}
			}
			if (Input.GetButtonDown ("Jump")) {
				animator.SetTrigger("isJump");
				rigid.AddForce (Vector3.up * jumpMultiplier);
			}
			//Roll code
			if(Input.GetButtonDown("Roll")){
				animator.SetTrigger("isRoll");
			}
			
			speed = new Vector2(leftX,leftY).sqrMagnitude;

			StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed);
			if (leftX > .2 || leftX < -.2 || leftY < -0.2 || leftY > 0.2){
				speed = 1;
			}
			animator.SetFloat("Speed", setSpeed(speed));
			animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);
		}
	}

	
	public void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut)
	{
		Vector3 rootDirection = root.forward;
		
		Vector3 stickDirection = new Vector3(leftX, 0, leftY);
		
		speedOut = stickDirection.sqrMagnitude;		
		
		// Get camera rotation
		Vector3 CameraDirection = camera.forward;
		CameraDirection.y = 0.0f; // kill Y
		Quaternion referentialShift = Quaternion.FromToRotation(Vector3.forward, Vector3.Normalize(CameraDirection));
		
		// Convert joystick input in Worldspace coordinates
		Vector3 moveDirection = referentialShift * stickDirection;
		Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);
		
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), moveDirection, Color.green);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), rootDirection, Color.magenta);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2f, root.position.z), stickDirection, Color.blue);
		Debug.DrawRay(new Vector3(root.position.x, root.position.y + 2.5f, root.position.z), axisSign, Color.red);
		
		float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.y += angleRootToMove;
		if(speed > .1 && !animator.GetBool("isLatched")){
			transform.eulerAngles = eulerAngles;
		}
		
		angleRootToMove /= 180f;
		
		directionOut = angleRootToMove * directionSpeed;
	}
	private float setSpeed(float speed){
		if (Input.GetButton ("Walk")) {
			speed *= 1;
			return speed;
		} 
		else if(Input.GetButton ("Sprint") && !exhausted && playerStamina.currentStamina >= 15){
			speed *= 3;
			return speed;
		}
		else{
			speed *= 2;
			return speed;
		}
	}
//
//	private bool IsInTheAir(){
//		return (Physics.Raycast(player.transform.position, -Vector3.up, distanceToTheGround + 0.1f));
//	}

}
