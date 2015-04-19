using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ContinueMenu : MonoBehaviour {
	public Button restartButton;
	//public Button mainMenuButton;
	public Button quitButton;
	public Canvas continueOverlay;
	public PlayerHealth playerHealth;
	public GameObject playerCharacter;
	public bool playerDead;

	public inGameMenuController inGameMenu;
	public GameObject inGameMenuObject;

	public GameObject hud;
	public Canvas hudCanvas;
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
		continueOverlay.enabled = false;
		Time.timeScale = 1;
		inGameMenuObject = GameObject.FindGameObjectWithTag ("inGameMenu");
		inGameMenu = inGameMenuObject.GetComponent<inGameMenuController> ();
		hud = GameObject.FindGameObjectWithTag ("HUD");
		hudCanvas = hud.GetComponent<Canvas> ();
	}

	IEnumerator GameOver()
	{
		yield return new WaitForSeconds(2);
		continueOverlay.enabled = true;
		Screen.lockCursor = false;
		Time.timeScale = 0;
		hudCanvas.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (playerHealth.isDead) {
			StartCoroutine (GameOver ());
		}
	}
}
