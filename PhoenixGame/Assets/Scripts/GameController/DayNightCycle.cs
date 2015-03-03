using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	/*
	 * Script written by Jonny Nabors
	 * Takes as public variables the length of the day and the rotation speed
	 * Rotates the sun & moon on the X axis by the rotationSpeed.
	 * */

	public float lengthOfDay;
	public float rotationSpeed;

	//public Material daySky = Resources.Load ("Sunny1 Skybox", typeof(Material)) as Material;
	private Transform sun;
	private Transform moon;
	public Material daySky, nightSky, eveningSky;
	private Vector3 objectAngles;

	void Update () {
		//Material daySky = Resources.Load ("Standard Assets/Skyboxes/Sunny3", typeof(Material)) as Material;
		//Get Rotation Speed
		rotationSpeed = Time.deltaTime / lengthOfDay;
		//Declare game objects
		sun = GameObject.FindGameObjectWithTag(Tags.sun).transform;
		moon = GameObject.FindGameObjectWithTag(Tags.moon).transform;
		//Rotate the sun and moon
		sun.Rotate(rotationSpeed, 0, 0);
		moon.Rotate(rotationSpeed, 0, 0);
		objectAngles = sun.rotation.eulerAngles;
		if(objectAngles.x <= 40)
			RenderSettings.skybox = eveningSky;
		else if(objectAngles.x >= 190)
			RenderSettings.skybox = nightSky;
		else
			RenderSettings.skybox = daySky;


	}
}
