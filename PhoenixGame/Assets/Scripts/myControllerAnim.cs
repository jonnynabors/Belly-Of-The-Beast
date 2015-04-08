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
	private myCamera gamecam;
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
	private float jumpMultiplier = 1f;
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
	
	}

	void Update() 
	{
		if (animator) {	
			
			// Pull values from controller/keyboard
			leftX = Input.GetAxis ("Horizontal");
			leftY = Input.GetAxis ("Vertical");	

			if (Input.GetButtonDown ("dashRight")) {
				animator.SetTrigger("dashRight");
			}
			if (Input.GetButtonDown ("dashLeft")) {
				animator.SetTrigger("dashLeft");
			}
			if (Input.GetButtonDown ("Jump")) {
				animator.SetTrigger("isJump");
			}
			
			speed = new Vector2(leftX,leftY).sqrMagnitude;

			StickToWorldspace(this.transform, gamecam.transform, ref direction, ref speed);
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
