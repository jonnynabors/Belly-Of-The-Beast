using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VictoryMenu : MonoBehaviour {

	public Canvas victoryOverlay;
	public PlayerHealth playerHealth;
	public GameObject playerCharacter;
	public bool isActive;

	public void restartPress()
	{
		Time.timeScale = 1;
		//loads first level
		Application.LoadLevel (1);
	}
	
	public void mainMenuButtonPress()
	{
		Application.LoadLevel (0);
	}
	
	public void quitButtonPress()
	{
		Application.Quit ();
	}
	
	// Use this for initialization
	void Start () 
	{
		playerCharacter = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = playerCharacter.GetComponent<PlayerHealth> ();
		victoryOverlay.enabled = false;
		Time.timeScale = 1;

	}
	
	IEnumerator playerWon()
	{
		yield return new WaitForSeconds(0);
		victoryOverlay.enabled = true;
		Screen.lockCursor = false;
		Time.timeScale = 0;
		isActive = true;
	}

	public void callVictoryMenu()
	{
		StartCoroutine (playerWon ());
	}
}
