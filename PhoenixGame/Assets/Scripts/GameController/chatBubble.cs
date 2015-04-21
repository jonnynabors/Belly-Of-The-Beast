using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class chatBubble : MonoBehaviour {
	public float range;
	public bool closeEnough;
	public GameObject NPC;
	public Vector3 vec;
	public GameObject Player;

	public GameObject DialogueCanvas;
	public Transform target;
	public float speed;
	public Image backgroundImage;

	public Text[] dialogue;
	public bool dialogueComplete;
	public bool enemiesCleared;

	//call the animator for friendly npc to talk
	public Animator anim;

	public int firstTextTalk = 0;

	//will change the bool for the NPC animation to make him
	// 'hurray' when all enemies are defeated.
	public int enemiesDefeated=0;
	public int enemiesTotal=1;

	/// flags for maintaining order&flow of dialogue
	/// flag1 is for the first string of dialogue, flag2 is the 2nd, and so on..
	/// they are set to true if the player has interacted with that string, so that
	/// the dialogue will continue to the next string
	public bool flag1, flag2;

	// Use this for initialization
	void Awake () {
		Player = GameObject.FindGameObjectWithTag("Player");
		anim = NPC.GetComponent<Animator> ();
		// initialize dialogue to first string
		dialogue [0].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		bool closeEnough = detectRange ();
		rotate ();
		//if player is not in range, deactivate chat bubble, and reset firstTextTalk
		if (!closeEnough)
		{
			firstTextTalk = 0;
			if (enemiesDefeated >= enemiesTotal)
				anim.SetBool("victorious", true);
			flagReset();
			disableDialogues ();
			GameObject.FindGameObjectWithTag("Bubble").GetComponent<Image>().enabled = false;
		}

		// enable appropriate dialogue string according to flags, and
		// call flagger if player is in range and F (Interact) is pushed
		else
		{
			//will trigger talk animation for the first dialogue when in range
			if (firstTextTalk == 0)
			{
				anim.SetTrigger("talk");
				firstTextTalk = 1;
			}
			//enables text to be displayed
			textEnabler();

			//call Flagger to change displayed dialogue when f is pressed.
			GameObject.FindGameObjectWithTag("Bubble").GetComponent<Image>().enabled = true;
			if (Input.GetKeyDown("f"))
			{
				flagger ();
				//if dialogue isn't complete, trigger talk animation
				if(!dialogueComplete)
					anim.SetTrigger("talk");
			}
		}
	}

	// sets text object's to being disabled so the player cannot see them
	public void disableDialogues()
	{
		for (int i=0; i<dialogue.Length; i++)
			dialogue[i].enabled = false;
	}

	// enables the text object according to which flags are set
	public void textEnabler()
	{

		//checks for victory state, and will alter animation bool
		if (enemiesDefeated >= enemiesTotal)
		{
			anim.SetBool ("victorious", true);
			dialogue[0].enabled = false;
			dialogue[1].enabled = false;
			dialogue[2].enabled = false;
			dialogue[3].enabled = true;
		}
		else
		{
			// if no interaction has yet occured, but player is within range,
			// enable first string of dialogue
			if (!flag1 && !flag2)
				dialogue[0].enabled = true;

			else if (flag1 && !flag2)
			{
				dialogue[0].enabled = false;
				dialogue[1].enabled = true;
				dialogue[2].enabled = false;
			}
			else if (flag1 && flag2)
			{
				dialogue[0].enabled = false;
				dialogue[1].enabled = false;
				dialogue[2].enabled = true;
				dialogueComplete = true;
			}
			else;
		}
	}

	// marks the appropriate flags for how much the player has
	// interacted with the friendly NPC so that the correct string is
	// displayed
	public void flagger()
	{
		// if flag1 is false, when this block is called by the interact keypress button,
		// mark flag1 as true, and so on
		if (!flag1)
			flag1 = true;
		else if (flag1 && !flag2)
			flag2 = true;
	}
	
	// when the player moves out of the NPC's range,
	// reset all flags to false to start dialogue from the beginning
	public void flagReset()
	{
		flag1 = false;
		flag2 = false;
		dialogueComplete = false;
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