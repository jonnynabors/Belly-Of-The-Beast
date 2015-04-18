using UnityEngine;
using System.Collections;

public class WhaleMoveController : MonoBehaviour {

	public float swimSpeed = 2f;
	public Transform[] targetWaypoints;
	public Animation anima;
	public AudioClip triggerAudioEvent;

	private NavMeshAgent navagent;
	private int waypointDestination;
	private GameObject player;
	private GameObject whale;
	private bool isDead = false;

	// Use this for initialization
	void Awake () {
		navagent = GetComponent<NavMeshAgent>();
		player = GameObject.FindGameObjectWithTag(Tags.player);
		whale = GameObject.FindGameObjectWithTag(Tags.whale);
		anima = whale.GetComponent<Animation>(); //Get build in animation components
	}
	
	// Update is called once per frame
	void Update () {
		//Call the patrol method
		if(!isDead)
		{
			Patrol();
		}
	}

	void Patrol()
	{
		//set the public value for the swim speed of the whale
		navagent.speed = swimSpeed;

		if(navagent.remainingDistance < navagent.stoppingDistance)
		{
			//If you are at the end of the array, restart
			if(waypointDestination == targetWaypoints.Length - 1)
				waypointDestination = 0;
			else
				waypointDestination++;
		}
			//Propel whale towards next waypoint location
			navagent.destination = targetWaypoints[waypointDestination].position;
	}

	//On trigger event for character colliding with whale object
	void OnTriggerEnter(Collider other)
	{
		//If collision occurs with player
		if(other.gameObject == player)
		{
			//AudioSource.PlayClipAtPoint(triggerAudioEvent, transform.position);
			//Play death animation
			anima.Play ("death");
			whale.GetComponent<Rigidbody>().velocity = Vector3.zero; //Stop movement
			isDead = true; //Set flag of is dead to true
		}
	}

}
