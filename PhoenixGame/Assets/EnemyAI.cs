using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
	public Transform currentEnemy;

	private NavMeshAgent nav;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.
	private float chaseTimer;                               // A timer for the chaseWaitTime.
	private float patrolTimer;                              // A timer for the patrolWaitTime.
	private int wayPointIndex = 0;                              // A counter for the way point array.
	private float distanceToPlayer = 0;						// Hold distance from the player.
	Animator anim;											//The game character's animation
	private RaycastHit hit;									//Calculate if enemy is hitting character

	//Tom's code
	public float timeBetweenAttacks = 1.0f;
	public int attackDamage = 10;
	GameObject playerCharacter;
	PlayerHealth playerHealth;
	public bool playerInRange;
	public float timer;

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


	void Awake(){
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = playerCharacter.GetComponent<PlayerHealth>();
	}

	void Attack()
	{
		timer = 0f;
		if (playerHealth.currentHealth > 0)
			playerHealth.TakeDamage (attackDamage);
	}
	//End of Tom's Code


	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		anim = GetComponent<Animator>();
		nav.autoBraking = false; //For continuous movement
	}
	
	// Update is called once per frame
	void Update () {
		// Continuously patrol
		if(nav.remainingDistance < 0.1f)
			Patrolling ();

		//Calculate distance to player
		//If within range, execute chase function
		distanceToPlayer = GetDistanceToPlayer();
		if(distanceToPlayer < 0.6)
			Chasing();

		//Tom's Code
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && playerInRange)
						Attack ();
	}

	void Patrolling ()
	{
		//If no waypoints set, return
		if (patrolWayPoints.Length == 0)
			return;
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		anim.SetBool ("EnemyRunning", false);
		anim.SetBool ("EnemyWalking", true);
		//Tell NavAgent to go to next destination
		nav.destination = patrolWayPoints[wayPointIndex].position;
		//Increment waypoint array index
		wayPointIndex = (wayPointIndex + 1) % patrolWayPoints.Length;
	}

	//Calculate the distance between the player and the enemy
	float GetDistanceToPlayer()
	{
		return Vector3.Distance(player.position, currentEnemy.position);
	}

	//Chase after the game player
	void Chasing()
	{
		//anim.SetBool ("EnemyWalking", false);
		anim.SetBool ("EnemyRunning", true);
		nav.SetDestination(player.position);
	}
}