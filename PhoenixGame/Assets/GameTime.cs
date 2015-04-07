using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {

	public enum TimeOfDay{
		Idle,
		SunRise,
		SunSet,
	}

	public Transform[] sun;
	private SunScript[] sunScript;
	public float dayCycleInMinutes = 1;		//Real time for how long a day lasts in game
	public float startTime; 				//Time of day to start in military time

	public float sunRise;					//Time of day that sunrise starts
	public float sunSet;					//Time of day that sunset starts
	public float skyBoxBlendModifier;		//Speed that the skybox textures blend

	public Color ambLightMax;				//Color of ambient light at day
	public Color ambLightMin;				//Color of ambient light at night

	public float morningLight;				//What time of day should morning lights happen
	public float nightLight;				//What time of day should night lights happen
	public bool isMorning = false;			//Determine if it is morning


	private const float second = 1;
	private const float minute = 60 * second;
	private const float hour = 60 * minute;
	private const float day = 24 * hour;
	private const float degreesPerSecond = 360 / day;
	private float dayCycleInSeconds;

	private float degreeRotation;
	private float timeOfDay;

	private TimeOfDay _tod;
	private float _noonTime;				//Time of day when it should be noon
	private float _morningLength;			//Sunrise to noon
	private float _eveningLength;			//Noon to sunset

	// Use this for initialization
	void Start () {
		timeOfDay = 0;
		_tod = TimeOfDay.Idle;
		dayCycleInSeconds = dayCycleInMinutes * minute;
		RenderSettings.skybox.SetFloat ("_Blend", 0);
		degreeRotation = degreesPerSecond * day / dayCycleInSeconds;
		sunScript = new SunScript[sun.Length];

		//Apply all sunscripts to sun objects.
		for(int j = 0; j < sun.Length; j++)
		{
			SunScript tempValue = sun[j].GetComponent<SunScript>();
			if(tempValue == null)
			{
				Debug.LogWarning("No Sun Script Found");
				sun[j].gameObject.AddComponent<SunScript>();
				tempValue = sun[j].GetComponent<SunScript>();
			}
			sunScript[j] = tempValue;
		}

		sunRise *= dayCycleInSeconds;
		sunSet *= dayCycleInSeconds;
		_noonTime = dayCycleInSeconds / 2;	//calculate when noon is
		_morningLength = _noonTime - sunRise;	//get length of morning in seconds
		_eveningLength = sunSet - _noonTime;	//get length of evening in seconds
		morningLight *= dayCycleInSeconds;
		nightLight *= dayCycleInSeconds;

		SetupLighting();	//Setup lighting to minlight values to start
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < sun.Length; i++)
		sun [i].Rotate (new Vector3(degreeRotation, 0, 0) * Time.deltaTime); //Rotate sun on x-axis
		timeOfDay = timeOfDay + Time.deltaTime;

		//Can add number of days/weeks here
		if (timeOfDay > dayCycleInSeconds)
			timeOfDay -= dayCycleInSeconds;

		//Control outside lighting effects based on time of day
		if(!isMorning && timeOfDay > morningLight && timeOfDay < nightLight)
		{
			isMorning = true;
			for(int i = 0; i < GameObject.FindGameObjectsWithTag("DayLight").Length; i ++)
			{
				GameObject.FindGameObjectsWithTag("DayLight")[i].GetComponent<Light>().enabled = true;
			}
			for(int i = 0; i < GameObject.FindGameObjectsWithTag("NightLight").Length; i ++)
			{
				if(GameObject.FindGameObjectsWithTag("NightLight")[i].GetComponent<Light>())
				GameObject.FindGameObjectsWithTag("NightLight")[i].GetComponent<Light>().enabled = false;
			}
		}
		else if(isMorning && timeOfDay > nightLight)
		{
			isMorning = false;
			for(int i = 0; i < GameObject.FindGameObjectsWithTag("DayLight").Length; i ++)
			{
				GameObject.FindGameObjectsWithTag("DayLight")[i].GetComponent<Light>().enabled = false;
			}
			for(int i = 0; i < GameObject.FindGameObjectsWithTag("NightLight").Length; i ++)
			{
				if(GameObject.FindGameObjectsWithTag("NightLight")[i].GetComponent<Light>())
				GameObject.FindGameObjectsWithTag("NightLight")[i].GetComponent<Light>().enabled = true;
			}
		}

		if(timeOfDay > sunRise && timeOfDay < _noonTime)
		{
			AdjustLighting(true);
		}
		else if(timeOfDay > _noonTime && timeOfDay < sunSet)
		{
			AdjustLighting(false);
		}


		//If time of day > sunrise and < sunset and skybox is not fully blended
		if(timeOfDay > sunRise && timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend") < 1)
		{
			_tod = GameTime.TimeOfDay.SunRise;
			BlendSkybox ();
		}
		else if(timeOfDay > sunSet && RenderSettings.skybox.GetFloat("_Blend") > 0){
			_tod = GameTime.TimeOfDay.SunSet;
			BlendSkybox();
		}
		else{
			_tod = GameTime.TimeOfDay.Idle;
		}
	}

	private void BlendSkybox()
	{
		float temp = 0;
		switch(_tod)
		{
		case(TimeOfDay.SunRise):
			temp = (timeOfDay - sunRise) / dayCycleInSeconds * skyBoxBlendModifier;
			break;
		case (TimeOfDay.SunSet):
			temp = (timeOfDay - sunSet) / dayCycleInSeconds * skyBoxBlendModifier;
			temp = 1 - temp;
			break;
		}

		RenderSettings.skybox.SetFloat ("_Blend", temp);
	}

	private void SetupLighting(){
		RenderSettings.ambientLight = ambLightMin;

		for(int i = 0; i < sunScript.Length; i ++)
		{
			if(sunScript[i].giveLight)
			{
				sun[i].GetComponent<Light>().intensity = sunScript[i].minLightBrightness;
			}
		}
	}


	//Handle brightness of the sun increasing and decreasing during the day
	private void AdjustLighting(bool brighten)
	{
		float pos = 0;
		if(brighten)
		{
			pos = (timeOfDay - sunRise) / _morningLength;		//get position of sun in morning sky
		}
		else
		{
			pos = (sunSet - timeOfDay) / _eveningLength;	//get pos of sun in evening sky
		}

		RenderSettings.ambientLight = new Color(ambLightMin.r + ambLightMax.r * pos,
		                                        ambLightMin.g + ambLightMax.g * pos,
		                                        ambLightMin.b + ambLightMax.b * pos);

		for(int i = 0; i < sunScript.Length; i ++)
		{
			if(sunScript[i].giveLight)
			{
				sunScript[i].GetComponent<Light>().intensity = sunScript[i].maxLightBrightness * pos;
			}
		}
	}
}
