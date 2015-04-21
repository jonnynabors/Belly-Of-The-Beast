// The speed of the player up and down the ladder. Roughly equal to walking speed is a good starting point.
var climbSpeed = 6.0;
// In the range -1 to 1 where -1 == -90 deg, 1 = 90 deg, angle of view camera at which the user climbs down rather than up when moving with the forward key.
var climbDownThreshold = -0.4;

public var climbDirection : Vector3 = Vector3.zero;
public var lateralMove : Vector3 = Vector3.zero;
public var ladderMovement : Vector3 = Vector3.zero;
public var ladderEnd : Vector3 = new Vector3(0.6f, 0.6f,0);
public var currentLadder : Ladder = null;
public var latchedToLadder : boolean = false;
public var inTopLandingPad : boolean = false;
public var inBottomLandingPad : boolean = false;
public var landingPads : ArrayList = null; 
public var charTransform : Transform;
public var anim : Animator;
public var charBody : Rigidbody;
public var weaponAway : boolean = false;
public var sword : GameObject;
public var shield: GameObject;


function Start () {
	charTransform = GetComponent(Transform);
	//myControllerAnim = GetComponent(myControllerAnim);
	mainCamera = GameObject.FindWithTag("MainCamera");
	controller = GetComponent(CharacterController);
	landingPads = new ArrayList();
}

function OnTriggerEnter (other : Collider) {
	if(other.tag == "TopLadderLandingPadExt") {
		inTopLandingPad = true;
		anim.SetBool("topLadderExt", true);
		anim.SetTrigger("Ladder");
		if(!latchedToLadder && !weaponAway){
			anim.SetTrigger("weaponBack");
			sword.GetComponent(MeshRenderer).SetActive(false);
			shield.GetComponent(MeshRenderer).SetActive(false);
			weaponAway = true;
		}
	}
	if(other.tag == "BottomLadderLandingPad") {
		inBottomLandingPad = true;
		anim.SetBool("botLadder", true);
		anim.SetTrigger("Ladder");
	}
	if(other.tag == "TopLadderLandingPad") {
		inBottomLandingPad = true;
		anim.SetBool("topLadder", true);
		anim.SetTrigger("Ladder");
	}
	
	if(other.tag == "Ladder") {	
		anim.applyRootMotion = false;
		charBody.useGravity = false;
		LatchLadder(other.gameObject, other);
		anim.SetBool("isLatched", true);
	}
	if(other.tag == "BottomLadderLandingPadExt") {
		anim.SetBool("botLadderExt", true);
		if(!latchedToLadder && !weaponAway){	
			anim.SetTrigger("weaponBack");
			sword.GetComponent(MeshRenderer).SetActive(false);
			shield.GetComponent(MeshRenderer).SetActive(false);
			weaponAway = true;
		}	
	}
}

function OnTriggerExit (other : Collider) {		
	if(other.tag == "TopLadderLandingPadExt") {
		inTopLandingPad = false;
		anim.SetBool("topLadderExt", false);
		if(!latchedToLadder && weaponAway){
			anim.SetTrigger("takeWeapon");
			sword.GetComponent(MeshRenderer).SetActive(true);
			shield.GetComponent(MeshRenderer).SetActive(true);
			weaponAway = false;
		}
	}
	if(other.tag == "BottomLadderLandingPad") {
		inBottomLandingPad = false;
		anim.SetBool("botLadder", false);
		
	}
	if(other.tag == "TopLadderLandingPad") {
		inBottomLandingPad = false;
		anim.SetBool("topLadder", false);
		ladderEnd += charTransform.position;
		//charTransform.Translate(ladderEnd	 * 2 * Time.deltaTime);
	}
	if(other.tag == "BottomLadderLandingPadExt") {
		anim.SetBool("botLadderExt", false);
		if(!latchedToLadder && weaponAway){
			anim.SetTrigger("takeWeapon");
			sword.GetComponent(MeshRenderer).SetActive(true);
			shield.GetComponent(MeshRenderer).SetActive(true);
			weaponAway = false;	
		}
	}

	if(other.tag == "Ladder") {
		anim.applyRootMotion = true;
		charBody.useGravity = true;
		UnlatchLadder();
		anim.SetBool("isLatched", false);
	}
}

/**
 *	Connect the player to the ladder, and shunt FixedUpdate calls to the special ladder movment functions.
 */
function LatchLadder (latchedLadder : GameObject, collisionWaypoint : Collider) {
	// typecast the latchedLadder as a Ladder object from GameObject
	currentLadder = latchedLadder.GetComponent(Ladder);
	latchedToLadder = true;
	
	// get the climb direction
	climbDirection = currentLadder.ClimbDirection();
	
	// let the other scripts know we are on the ladder now
	gameObject.SendMessage("OnLadder", null, SendMessageOptions.RequireReceiver);
}

function UnlatchLadder () {
	latchedToLadder = false;
	currentLadder = null;
	
	gameObject.SendMessage("OffLadder", ladderMovement, SendMessageOptions.RequireReceiver);
	
		
}


function Update () {
	

	verticalMove = climbDirection.normalized;
	

	verticalMove *= Input.GetAxis("Vertical");

	anim.SetFloat("ladderDir", Input.GetAxis("Vertical"));
	// move
	//ladderMovement = verticalMove + lateralMove;
	lateralMove = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
	ladderMovement = charTransform.position;
	ladderMovement.x = verticalMove.x + lateralMove.x;
	ladderMovement.y = verticalMove.y;
	ladderMovement.z = verticalMove.z;
	
	if(latchedToLadder){
		charTransform.Translate(ladderMovement * climbSpeed * Time.deltaTime);	
	}
		
}

//@script RequireComponent(CharacterController)