using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class inGameMenuController : MonoBehaviour {
	
	public Canvas mainMenu;
	public Canvas subMenu;
	public Canvas quitMenu;
	public GameObject optionsCanvas;
	public GameObject controlsCanvas;
	public GameObject exitCanvas;
	public Button mainButton;
	public Button returnButton;
	public Button optionsButton;
	public Button controlsButton;
	public Button exitButton;
	public myCamera cameraScript;
	private bool isInMenu;

	
	
	// Use this for initialization
	void Start (){
		//grab components
		mainMenu = mainMenu.GetComponent<Canvas> ();
		subMenu = subMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		mainButton = mainButton.GetComponent<Button> ();
		returnButton = returnButton.GetComponent<Button> ();
		optionsButton = optionsButton.GetComponent<Button> ();
		controlsButton = controlsButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();

		//Makes quit menu NOT visible
		isInMenu = false;
		mainMenu.enabled = false;
		quitMenu.enabled = false;
		subMenu.enabled = false;
		Time.timeScale = 1;
	}
	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			if (isInMenu)
			{
				isInMenu = false;
				Screen.lockCursor = true;
				mainMenu.enabled = false;
				subMenu.enabled = false;
				cameraScript.enabled = true;
				Time.timeScale = 1;
			}
			else
			{
				isInMenu = true;
				mainMenu.enabled = true;
				cameraScript.enabled = false;
				Screen.lockCursor = false;
				Time.timeScale = 0;
			}
		}
	}

	public void mainPress(){
		Time.timeScale = 0;
		//loads first level
		Application.LoadLevel (0);
	}
	
	public void returnPress(){
		Time.timeScale = 1;
		cameraScript.enabled = true;
		mainMenu.enabled = false;
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
	
	//exitPress
	public void ExitPress(){
		//Makes quit menu visible and disables our main menu buttons
		quitMenu.enabled = true;
		mainButton.enabled = false;
		returnButton.enabled = false;
		optionsButton.enabled = false;
		controlsButton.enabled = false;
		exitButton.enabled = false;
		exitCanvas.SetActive (true);
	}
	
	//noButton Exit Warning
	public void NoPress(){
		//re-enable our main menu buttons and hide quitmenu
		quitMenu.enabled = false;
		mainButton.enabled = true;
		returnButton.enabled = true;
		optionsButton.enabled = true;
		controlsButton.enabled = true;
		exitButton.enabled = true;
		exitCanvas.SetActive (false);
	}
	
	//yesButton Exit Warning	
	public void ExitGame(){
		Application.Quit ();
	}
	
}
