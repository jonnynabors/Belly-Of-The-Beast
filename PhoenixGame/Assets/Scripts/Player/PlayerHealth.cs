using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
	public int startingHealth = 200;
	public int currentHealth;
	public Slider healthSlider;
	bool isDead;
	//bool damaged;
	//reference to player control script
	ThirdPersonController playerController;
	ThirdPersonCamera playerCamera;

	void Awake()
	{
		//assign script and currentHealth upon game start
		playerController = GetComponent<ThirdPersonController> ();
		playerCamera = GetComponent<ThirdPersonCamera> ();
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount)
	{
		currentHealth -= amount;
		healthSlider.value = currentHealth;

		if (currentHealth <= 0 && !isDead)
			Death ();
	}

	void Death()
	{
		isDead = true;
		playerController.enabled = false;
		playerCamera.enabled = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
