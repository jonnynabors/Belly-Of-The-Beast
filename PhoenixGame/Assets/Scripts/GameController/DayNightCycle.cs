using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {

	public float lengthOfDay;
	public float rotationSpeed;

	private Transform sun;
	private Transform moon;

	void Update () {
		rotationSpeed = Time.deltaTime / lengthOfDay;
		sun = GameObject.FindGameObjectWithTag(Tags.sun).transform;
		moon = GameObject.FindGameObjectWithTag(Tags.moon).transform;
		sun.Rotate(0, rotationSpeed, 0);
		moon.Rotate(0, rotationSpeed, 0);
	}
}
