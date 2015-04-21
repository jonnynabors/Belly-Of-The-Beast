using UnityEngine;
using System.Collections;
/*
 * Written by: Jonny Nabors
 * Date Created: 4/1/2015
 * Date Edited:
 * Purpose: Calculate incoming damage given to Enemy
 * by the player. Handle dessence drop and Enemy death
 * animation(s)
 */
public class EnemyHealth : MonoBehaviour {
	public int startingHealth = 150;						//Starting health for Enemy prefab
	public GameObject playerCharacter;						//Reference to Player GameObject
	public bool playerInRange;								//Check if player is touching enemy
	public EnemyAI enemyAIScript;							//Reference to EnemyAI Script
	public Transform essencePrefab;							//Reference to Essence Prefab
	public int essencesToDrop = 10;							//Amount of Essences to drop
	private NavMeshAgent nav;                               // Reference to the nav mesh agent.

	int atackState = Animator.StringToHash ("Base.Attack1");
	Animator anim;											//Enemy's Animator Controller
	int currentHealth;										//Value of enemy's current health
	Vector3 dropLocation;									//Location to drop Essences at

	// Use this for initialization
	void Start () {
		//Initialize variables
		currentHealth = startingHealth;
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		//Check if player is in range, if so then take damage
//		if (playerInRange)
//			EnemyTakeDamage (1);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == playerCharacter)
			playerInRange = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == playerCharacter)
			playerInRange = false;

		//anim.SetBool ("EnemyAttacking", false);
	}

	//Assign damage to the enemy
	public void EnemyTakeDamage(int damageTaken)
	{
		anim.SetBool ("EnemyAttacking", true);

		AnimatorStateInfo currentBaseState = anim.GetCurrentAnimatorStateInfo(0);
		if(currentBaseState.nameHash == atackState)
		{
			playerCharacter.GetComponent<Rigidbody>().AddForce(transform.position + transform.forward * 20);
		}
		else{
			anim.SetBool ("Attack1Complete", true);
		}

		if ((currentHealth -= damageTaken) <= 0) {
			currentHealth = 0;
			EnemyDeath();
		}
		anim.SetTrigger ("EnemyHit");
		Debug.Log (currentHealth);
	}

	public void EnemyDeath()
	{
		anim.SetBool ("IsDead", true);
		//anim.SetTrigger ("DeadTrigger");
		nav.enabled = false;
		enemyAIScript.enabled = false;
		GetComponent<Rigidbody>().detectCollisions = false;
		StartCoroutine (ClearGameObject ());
	}

	//Give the animation time to resolve
	IEnumerator ClearGameObject()
	{
		yield return new WaitForSeconds(3);
		DestroyObject();
	}

	//Destroy the enemy game object
	void DestroyObject()
	{
		Destroy (gameObject);
		int i = 0;
		while(i < essencesToDrop){
			dropLocation = new Vector3((Random.value / 2), -.1f, (Random.value / 2));
			Instantiate(essencePrefab, transform.position + dropLocation, transform.rotation);
			i++;
		}
	}
}
