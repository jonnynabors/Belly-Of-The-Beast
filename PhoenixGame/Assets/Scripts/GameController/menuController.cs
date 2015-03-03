using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class menuController : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;

	// Use this for initialization
	void Start () {
		//grab components
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();

		//Makes quit menu NOT visible
		quitMenu.enabled = false;
		Time.timeScale = 1;
	}

	public void ExitPress()
	{
		//Makes quit menu visible and disables our main menu buttons
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void NoPress()
	{
		//re-enable our main menu buttons and hide quitmenu
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel()
	{
		//loads first level
		Application.LoadLevel (1);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}

}
