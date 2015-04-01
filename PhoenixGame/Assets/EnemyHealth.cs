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
	public int startingHealth = 150;
	public GameObject playerCharacter;
	public float timer;
	public bool playerInRange;
	public float timeBetweenAttacks = 1.0f;
	public EnemyAI enemyAIScript;
	private NavMeshAgent nav;                               // Reference to the nav mesh agent.

	Animator anim;

	bool isDead= false;
	int currentHealth;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange)
			EnemyTakeDamage (50);
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
	}

	public void EnemyTakeDamage(int damageTaken)
	{
		timeBetweenAttacks = 0;
		if ((currentHealth -= damageTaken) <= 0) {
			currentHealth = 0;
			EnemyDeath();
		}

		//currentHealth -= damageTaken;
		Debug.Log (currentHealth);
	}

	public void EnemyDeath()
	{
		isDead = true;
		Debug.Log ("Dead");
		anim.SetBool ("IsDead", true);
		rigidbody.angularVelocity = Vector3.zero;
		rigidbody.isKinematic = true;
		nav.enabled = false;
		enemyAIScript.enabled = false;
		StartCoroutine (ClearGameObject ());
		//ClearGameObject ();
	}

	IEnumerator ClearGameObject()
	{
		yield return new WaitForSeconds(2);
		DestroyObject();
	}
	void DestroyObject()
	{
		Destroy (gameObject);
	}
}
