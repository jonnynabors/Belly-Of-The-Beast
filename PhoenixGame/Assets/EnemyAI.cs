using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
	public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
	public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
	public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
	public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.


	private NavMeshAgent nav;                               // Reference to the nav mesh agent.
	private Transform player;                               // Reference to the player's transform.
	private float chaseTimer;                               // A timer for the chaseWaitTime.
	private float patrolTimer;                              // A timer for the patrolWaitTime.
	private int wayPointIndex = 0;                              // A counter for the way point array.
	Animator anim;											//The game character's animation

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		anim = GetComponent<Animator>();
		nav.autoBraking = false; //For continuous movement
	}
	
	// Update is called once per frame
	void Update () {
		if(nav.remainingDistance < 0.1f)
			Patrolling ();
	}

	void Patrolling ()
	{
		//If no waypoints set, return
		if (patrolWayPoints.Length == 0)
			return;
		// Set an appropriate speed for the NavMeshAgent.
		nav.speed = patrolSpeed;
		anim.SetBool ("IsWalking", true);
		//Tell NavAgent to go to next destination
		nav.destination = patrolWayPoints[wayPointIndex].position;
		//Increment waypoint array index
		wayPointIndex = (wayPointIndex + 1) % patrolWayPoints.Length;
	}
}
