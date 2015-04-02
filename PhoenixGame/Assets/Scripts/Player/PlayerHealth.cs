using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 200;
	public int currentHealth;
	public Slider healthSlider;
	public bool isDead;
	//bool damaged;
	//reference to player control script
	myControllerAnim playerController;
	public GameObject playerCharacter;
	Rigidbody rb;

	void Start()
	{
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
	}

	void Awake()
	{
		//assign script and currentHealth upon game start
		playerController = GetComponent<myControllerAnim> ();
		currentHealth = startingHealth;
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

		updateHealthBar ();
	
	}

	void updateHealthBar(){
		healthSlider.value = currentHealth;
	}

	public void TakeDamage(int amount)
	{
		//checks so health is never negative
		if ((currentHealth -= amount) <= 0)
		{
			currentHealth = 0;
			Death ();
		}

		updateHealthBar ();
	}

	void Death()
	{
		isDead = true;
		//Destroy(playerCharacter);
	}
}
