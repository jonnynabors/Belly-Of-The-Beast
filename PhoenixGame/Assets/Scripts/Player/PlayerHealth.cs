using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 200;
	public int currentHealth;
	public bool isDead;

	//reference to player control script
	Animator playerController;
	public GameObject playerCharacter;
	Rigidbody rb;

	//reference another script
	public HealthBarMover healthBarMover;
	public GameObject healthBar;

	void Start()
	{
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		healthBar = GameObject.FindGameObjectWithTag ("HealthBar");
		healthBarMover = healthBar.GetComponent<HealthBarMover> ();
	}

	void Awake()
	{
		//assign script and currentHealth upon game start
		playerController = GetComponent<Animator> ();
		currentHealth = startingHealth;
	}

	//this script is called by the HealthBarMover
	public double healthPercentage()
	{
		return currentHealth / startingHealth;
	}
	
	public void potionUsed()
	{
		if ((currentHealth + 80) <= 200)
			currentHealth += 80;

		//protects against accidental use of potion.. skip
		else if (currentHealth == 200);

		//and restore hp to max hp (currently 200) if less than 80 health is missing
		else
			currentHealth = 200;

		healthBarMover.updateHealthBar ();
	
	}

	public void TakeDamage(int amount)
	{
		//checks so health is never negative
		if ((currentHealth -= amount) <= 0)
		{
			currentHealth = 0;
			Death ();
		}

		healthBarMover.updateHealthBar ();
	}

	void Death()
	{
		isDead = true;
		playerController.SetBool ("isDead", isDead);
		//Destroy(playerCharacter);
	}
}
