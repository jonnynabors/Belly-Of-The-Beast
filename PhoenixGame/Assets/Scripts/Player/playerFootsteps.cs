using UnityEngine;
using System.Collections;

public class playerFootsteps : MonoBehaviour {
	public bool inWater = false;
	public bool onGrass = false;
	public bool onWood = false;
	public bool onGravel = false;
	public bool onSand = false;
	public bool onStone = false;
	public AudioClip waterStepSound;
	public AudioClip woodStepSound;
	public AudioClip gravelStepSound;
	public AudioClip grassStepSound;
	public AudioClip sandStepSound;
	public AudioClip stoneStepSound;
	public float waterTimer = 0;
	public float waterCool = 0.6f;
	public AudioSource audio;

	public Animator anim;
	public bool onLadder = false;
	public bool onLadderMoving = false;
	public bool animState = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		onGravel = true;
//		onLadder = anim.GetBool ("isLatched");
//		if (anim.GetFloat ("ladderDir") > 0 || anim.GetFloat ("ladderDir") < 0) {
//						onLadderMoving = true;
//				} else {onLadderMoving = false;}
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion") || anim.GetCurrentAnimatorStateInfo (0).IsName ("climbing")) {
						animState = true;
			if (anim.GetFloat ("Speed") > 0) {
					if (inWater == true) {
							waterSound ();
					}

					if (onWood == true) {
							woodSound ();
					}
					if (onSand == true) {
							sandSound ();
					}
					if (onStone == true) {
							stoneSound ();
					}
					if (onGrass == true) {
							grassSound ();
					}
					if (onGravel == true) {
							gravelSound ();
					}
			}
			if (anim.GetFloat ("Speed") > 1) {
					waterCool = 0.4f;
					if (inWater == true) {
							waterSound ();
					}

					if (onWood == true) {
							woodSound ();
					}
					if (onSand == true) {
							sandSound ();
					}
					if (onStone == true) {
							stoneSound ();
					}
					if (onGrass == true) {
							grassSound ();	
					}
					if (onGravel == true && !onGrass) {
							gravelSound ();
					}
			} else {
					waterCool = 0.6f;		
			}
			if (waterTimer > 0) {
					waterTimer -= Time.deltaTime;
			}
			if (waterTimer < 0) {
					waterTimer = 0;
			}
		} else {animState = false;}
		
	}

	void waterSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(waterStepSound);		
			waterTimer = waterCool;
		}
	}
	void woodSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(woodStepSound);		
			waterTimer = waterCool;
		}
	}
	void gravelSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(gravelStepSound);		
			waterTimer = waterCool;
		}
	}
	void grassSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(grassStepSound);		
			waterTimer = waterCool;
		}
	}
	void sandSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(sandStepSound);		
			waterTimer = waterCool;
		}
	}
	void stoneSound(){
		if (waterTimer == 0) {
			audio.PlayOneShot(stoneStepSound);		
			waterTimer = waterCool;
		}
	}
}
