using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour {
	public int startingStamina = 150;
	public int currentStamina;
	public bool isMoving = false;
	//public bool isDead;
	public Animator anim;
	
	//reference to player control script
	Animator playerController;
	public GameObject playerCharacter;
	Rigidbody rb;
	
	//reference another script
	public StaminaBarMover staminaBarMover;
	public GameObject staminaBar;
	public float timer = 0f;
	public int regenRate = 10; 	//rate at which stamina is replenished per sec
	public float speed;
	public bool isBlocking;
	public bool isAttacking;
	public bool isDashing;

	//set to true when the player reaches 0 stamina,
	//restricting their movement (sprint, dash, etc)
	public bool exhausted; 
	
	void Start()
	{
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		staminaBar = GameObject.FindGameObjectWithTag ("StaminaBar");
		staminaBarMover = staminaBar.GetComponent<StaminaBarMover> ();
		anim = playerCharacter.GetComponent<Animator> ();
	
	}

	void Awake()
	{
		//assign script and currentHealth upon game start
		playerController = GetComponent<Animator> ();
		currentStamina = startingStamina;
	}

	public void potionUsed()
	{
		if ((currentStamina + 70) <= 150)
			currentStamina += 70;
		
		//and restore stamina to max stamina (currently 150) if less than 70 stamina is missing
		else
			currentStamina = 150;
		
		staminaBarMover.updateStaminaBar ();
	}
	
	void Death()
	{
		//isDead = true;
		//playerController.SetBool ("isDead", isDead);
		//Destroy(playerCharacter);
	}

	void regenerate()
	{
		if (timer > 1.0f)
		{
			//if already max, do nothing
			if ((currentStamina + regenRate) >= startingStamina)
				currentStamina = startingStamina;
			else
				currentStamina += regenRate;

			timer = 0f;
		}
	}

	void Running()
	{
		if ((currentStamina - 15) < 0)
			currentStamina = 0;
		else
			currentStamina -= 15;
	}

	void Block()
	{
		regenRate = 5;
	}

	void Attack()
	{
		if ((currentStamina - 30) < 0)
			currentStamina = 0;
		else
			currentStamina -= 30;
	}

	void Dash()
	{
		if ((currentStamina - 15) < 0)
			currentStamina = 0;
		else
			currentStamina -= 15;
	}

	void Update()
	{
		timer += Time.deltaTime;
		regenRate = 10;

		//2 second stamina depletion penalty
		if (currentStamina == 0 && !exhausted)
		{
			timer = -2.0f;
			exhausted = true;
		}

		if (timer > 1.0f) 
		{
			exhausted = false;
			//speed to distinguish between walk (1), jog (2), and sprint (3)
			speed = anim.GetFloat("Speed");

			//bool values represent which animation state the player character is in
			isMoving = anim.GetCurrentAnimatorStateInfo (0).IsName ("Locomotion");
			isBlocking = anim.GetCurrentAnimatorStateInfo (0).IsName ("Walking Block") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Block");
			isAttacking = anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack");
			isDashing = anim.GetCurrentAnimatorStateInfo (0).IsName ("Dash Left") || anim.GetCurrentAnimatorStateInfo (0).IsName ("Dash Right");

			//check for Running
			if (isMoving && speed == 3)
				Running ();

			else if (isBlocking)
			{
				Block();
				regenerate ();
			}

			else if (isAttacking)
				Attack ();

			else if (isDashing)
				Dash();

			else
				regenerate ();

			timer = 0f;
		}
		staminaBarMover.updateStaminaBar ();
		//currentStamina += avariable byte time;
	}
}
