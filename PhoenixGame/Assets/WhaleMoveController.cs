using UnityEngine;
using System.Collections;

public class WhaleMoveController : MonoBehaviour {

	public float swimSpeed = 2f;
	public Transform[] targetWaypoints;

	private NavMeshAgent navagent;
	private Transform whale;
	private int waypointDestination;
	// Use this for initialization
	void Awake () {
		navagent = GetComponent<NavMeshAgent>();
		whale = GameObject.FindGameObjectWithTag(Tags.whale).transform;
	}
	
	// Update is called once per frame
	void Update () {
		Patrol();
	}

	void Patrol()
	{
		navagent.speed = swimSpeed;
		if(navagent.remainingDistance < navagent.stoppingDistance)
		{
			if(waypointDestination == targetWaypoints.Length - 1)
				waypointDestination = 0;
			else
				waypointDestination++;
		}
			navagent.destination = targetWaypoints[waypointDestination].position;
	}
}
