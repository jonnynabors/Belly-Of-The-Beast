using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {

	public Transform[] sun;
	private SunScript[] sunScript;
	public float dayCycleInMinutes = 1;
	public float startTime; 				//Time of day to start in military time

	private const float second = 1;
	private const float minute = 60 * second;
	private const float hour = 60 * minute;
	private const float day = 24 * hour;
	private const float degreesPerSecond = 360 / day;
	private float dayCycleInSeconds;

	private float degreeRotation;
	private float timeOfDay;

	// Use this for initialization
	void Start () {
		timeOfDay = 0;
		dayCycleInSeconds = dayCycleInMinutes * minute;
		RenderSettings.skybox.SetFloat ("_Blend", 0);
		degreeRotation = degreesPerSecond * day / dayCycleInSeconds;
		sunScript = new SunScript[sun.Length];

		for(int j = 0; j < sun.Length; j++)
		{
			SunScript tempValue = sun[j].GetComponent<SunScript>();
			if(tempValue == null)
			{
				Debug.LogWarning("No Sun Script Found");
				sun[j].gameObject.AddComponent<SunScript>();
				tempValue = sun[j].GetComponent<SunScript>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < sun.Length; i++)
		sun [i].Rotate (new Vector3(degreeRotation, 0, 0) * Time.deltaTime); //Rotate sun on x-axis
		timeOfDay = timeOfDay + Time.deltaTime;


		//Can add number of days/weeks here
		if (timeOfDay > dayCycleInSeconds)
			timeOfDay -= dayCycleInSeconds;
		BlendSkybox ();
	}

	private void BlendSkybox()
	{
		float temp = timeOfDay / dayCycleInSeconds* 2;
		if (temp > 1)
			temp = 1 - (temp - 1);
		RenderSettings.skybox.SetFloat ("_Blend", temp);
	}
}
