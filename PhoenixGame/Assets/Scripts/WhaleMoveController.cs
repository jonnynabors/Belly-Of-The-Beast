using UnityEngine;
using System.Collections;

public class WhaleMoveController : MonoBehaviour {

	public float swimSpeed = 2f;
	public Transform[] targetWaypoints;

	private NavMeshAgent navagent;
	private int waypointDestination;

	// Use this for initialization
	void Awake () {
		navagent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		//Call the patrol method
		Patrol();
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
}
