using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuController : MonoBehaviour {

	public Canvas mainMenu;
	public Canvas subMenu;
	public Canvas quitMenu;
	public GameObject optionsCanvas;
	public GameObject controlsCanvas;
	public GameObject creditsCanvas;
	public GameObject exitCanvas;
	public Button playButton;
	public Button optionsButton;
	public Button controlsButton;
	public Button creditsButton;
	public Button exitButton;
	public GameObject hud;
	public Canvas hudCanvas;


	// Use this for initialization
	void Start (){
		//grab components
		mainMenu = mainMenu.GetComponent<Canvas> ();
		subMenu = subMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		playButton = playButton.GetComponent<Button> ();
		optionsButton = optionsButton.GetComponent<Button> ();
		controlsButton = controlsButton.GetComponent<Button> ();
		creditsButton = creditsButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();

		hud = GameObject.FindGameObjectWithTag ("HUD");
		hudCanvas = hud.GetComponent<Canvas> ();

		//Makes quit menu NOT visible
		quitMenu.enabled = false;
		subMenu.enabled = false;
	}

	//playButton
	public void playPress(){
		Time.timeScale = 1;
		//loads first level
		Application.LoadLevel (1);
		hudCanvas.enabled = true;
	}

	public void optionsPress(){
		mainMenu.enabled = false;
		subMenu.enabled = true;
		optionsCanvas.SetActive (true);
	}

	public void controlsPress(){
		mainMenu.enabled = false;
		subMenu.enabled = true;
		controlsCanvas.SetActive (true);
	}

	public void creditsPress(){
		mainMenu.enabled = false;
		subMenu.enabled = true;
		creditsCanvas.SetActive (true);
	}

	//exitPress
	public void ExitPress(){
		//Makes quit menu visible and disables our main menu buttons
		quitMenu.enabled = true;
		playButton.enabled = false;
		optionsButton.enabled = false;
		controlsButton.enabled = false;
		creditsButton.enabled = false;
		exitButton.enabled = false;
		exitCanvas.SetActive (true);
	}

	//noButton Exit Warning
	public void NoPress(){
		//re-enable our main menu buttons and hide quitmenu
		quitMenu.enabled = false;
		playButton.enabled = true;
		optionsButton.enabled = true;
		controlsButton.enabled = true;
		creditsButton.enabled = true;
		exitButton.enabled = true;
		exitCanvas.SetActive (false);
	}

	//yesButton Exit Warning	
	public void ExitGame(){
		Application.Quit ();
	}

}
