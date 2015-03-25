using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {

	public GameObject player;
	//bool value to flag that the chest has been collected
	public bool found;
	public GameObject potionCounterObject;
	PotionCounter potionCounterScript;
	int potionsFound = 5;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		potionCounterObject = GameObject.FindGameObjectWithTag ("PotionCounter");
		potionCounterScript = potionCounterObject.GetComponent<PotionCounter> ();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			if (found == false){
				found = true;
				potionCounterScript.potionCount += potionsFound;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
