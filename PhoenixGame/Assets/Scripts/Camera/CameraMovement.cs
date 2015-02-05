using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	public Transform player;            // The position that that camera will be following.
	public float smoothing = 5f;        // The speed with which the camera will be following.

	public int zoomSpeed = 5;
	public int maxFOV = 100;
	public int minFOV = 20;
	
	Vector3 offset;                     // The initial offset from the target.
	
	void Start ()
	{
		// Calculate the initial offset.
		player =  GameObject.FindGameObjectWithTag(Tags.player).transform;
		offset = transform.position - player.position;
	}
	
	void FixedUpdate ()
	{
		// Create a postion the camera is aiming for based on the offset from the target.
		Vector3 targetCamPos = player.position + offset;
		
		// Smoothly interpolate between the camera's current position and it's target position.
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing * Time.deltaTime);
	}

	void Update ()
	{
		Zoom ();
	}

	void Zoom()
	{
		float delta = Input.GetAxis ("Zoom") * -zoomSpeed;
		if (Input.GetButton ("Zoom")) {
			if((Camera.main.fieldOfView + delta > minFOV)&&(Camera.main.fieldOfView + delta < maxFOV))
			{
				Camera.main.fieldOfView += delta;
			}
		}
	}
}