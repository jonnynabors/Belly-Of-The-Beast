using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PotionCounter : MonoBehaviour {
	public Text potionCounter;
	public int potionCount=0;

	// Use this for initialization
	void Start () {
		potionCounter = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		potionCounter.text = "" + potionCount;
	}
}
