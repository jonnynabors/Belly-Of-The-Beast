using UnityEngine;
using System.Collections;

public class WhaleMoveController : MonoBehaviour {
	public Transform[] targetWaypoints;

	private NavMeshAgent navagent;
	public float swimSpeed = 2f;
	public float moveWaitTime = 1f;

	private int waypointDestination = 0;
	private float moveTimer = 0f;
	// Use this for initialization
	void Awake () {
		navagent = GetComponent<NavMeshAgent>();
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
		for(int i = 1; i < 3; i++)
		{
			navagent.destination = targetWaypoints[waypointDestination].position;
		}
	}
}
