using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMenu : MonoBehaviour {

	public Canvas quitMenu;
	public Button continueText;
	public Button mainText;
	public Button exitText;
	public Canvas startMenu;
	public ThirdPersonCamera cameraScript;

	private bool isInMenu = false;

	
	// Use this for initialization
	void Start () {
		//grab components
		quitMenu = quitMenu.GetComponent<Canvas> ();
		mainText = mainText.GetComponent<Button> ();
		continueText = continueText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		startMenu = startMenu.GetComponent<Canvas> ();
		cameraScript = cameraScript.GetComponent<ThirdPersonCamera> ();

		
		//Makes quit menu NOT visible
		quitMenu.enabled = false;
		startMenu.enabled = false;
	}

	void Update ()
	{
		if(Input.GetKeyDown (KeyCode.Escape))
		{
			if (isInMenu)
			{
				isInMenu = false;
				startMenu.enabled = false;
				cameraScript.enabled = true;
				Time.timeScale = 1;
			}
			else
			{
				isInMenu = true;
				startMenu.enabled = true;
				cameraScript.enabled = false;
				Time.timeScale = 0;
			}
		}
	}

	public void ExitPress()
	{
		//Makes quit menu visible and disables our main menu buttons
		quitMenu.enabled = true;
		mainText.enabled = false;
		continueText.enabled = false;
		exitText.enabled = false;
	}

	public void MainPress()
	{
		Application.LoadLevel (0);
	}

	public void ContinuePress()
	{
		startMenu.enabled = false;
		cameraScript.enabled = true;
		isInMenu = false;
	}

	public void NoPress()
	{
		//re-enable our main menu buttons and hide quitmenu
		quitMenu.enabled = false;
		mainText.enabled = true;
		continueText.enabled = true;
		exitText.enabled = true;
	}
	

	
	public void ExitGame()
	{
		Application.Quit ();
	}
	
}
