using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
 * Author: Jonny Nabors
 * Date Created: 3/29/2015
 * Date Last Edited: 
 * Purpose: To handle the behavior of the Essences dropped by 
 * enemies and found in the game. 
 */

public class EssenceController : MonoBehaviour {

	public Text essenceCounter;				//Text representation of essence counter
	public float moveSpeed = 0.75f;			//Speed at which the essence moves to character
	public Texture	sphereTexture;			//Texture of the Essence
	public AudioClip audioClip;
	private int count;						//Count of essence
	private float distanceToPlayer = 0f; 	//Distance between Essence & Character
	private Transform player; 				//Player gameObject


	void Start(){
		//Initialize variables
		essenceCounter = GameObject.FindGameObjectWithTag("EssenceCounter").GetComponent<Text>();
		player = GameObject.FindGameObjectWithTag(Tags.player).transform;
		gameObject.GetComponent<Renderer>().material.mainTexture = sphereTexture;
	}

	void Update () {
		transform.Rotate(new Vector3(15,30,45) * Time.deltaTime);	//Rotate the essence
		distanceToPlayer = GetDistanceToPlayer ();					//Calculate distance to player
		if(distanceToPlayer < 0.3)
			transform.position = Vector3.MoveTowards (transform.position, 
			                                          player.position, 
			                                          moveSpeed * Time.deltaTime);	//Move towards player
	}
	
	void OnTriggerEnter(Collider other) 
	{
		//Handle collision with player
		if(other.gameObject.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(audioClip, new Vector3(transform.position.x, transform.position.y, transform.position.z));
			count = int.Parse (essenceCounter.text); //Get current count
			count++; 								 //Increment essence count
			essenceCounter.text = count.ToString();  //Update count
			Destroy(gameObject);					 //Destroy Essence GameObject
		}
	}

	//Calculate distance to character
	public float GetDistanceToPlayer()
	{
		return Vector3.Distance(player.position, transform.position);
	}



}
