using UnityEngine;
using System.Collections;

public class TreasureChest : MonoBehaviour {

	public GameObject player;
	//bool value to flag that the chest has been collected
	public bool found;
	public GameObject potionCounterObject;
	public GameObject closedState;
	public GameObject openState;
	PotionCounter potionCounterScript;
	int potionsFound = 5;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		potionCounterObject = GameObject.FindGameObjectWithTag ("PotionCounter");
		potionCounterScript = potionCounterObject.GetComponent<PotionCounter> ();
		closedState = GameObject.FindGameObjectWithTag("CloseChest");
		openState = GameObject.FindGameObjectWithTag("OpenChest");
		openState.gameObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			openState.gameObject.SetActive(true);
			closedState.gameObject.SetActive(false);
			if (found == false){
				found = true;
				potionCounterScript.increasePotions(5);
			}
		}
	}
}
