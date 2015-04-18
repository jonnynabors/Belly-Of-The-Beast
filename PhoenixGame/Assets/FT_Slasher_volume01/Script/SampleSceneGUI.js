var mySkin : GUISkin;
var effect01 : GameObject;
var effect02 : GameObject;
var effect03 : GameObject;
var effect04 : GameObject;
var effect05 : GameObject;
var effect06 : GameObject;
var effect07 : GameObject;
var effect08 : GameObject;
var effect09 : GameObject;
var effect10 : GameObject;
var effect11 : GameObject;
var effect12 : GameObject;
var zombie : GameObject;


function OnGUI ()
{
	GUI.skin = mySkin;
	
	GUI.Label (Rect (70,10,100,20), "FT Slasher");

	if(GUI.Button (Rect (10,40,20,20), GUIContent ("", "Slash01")))
	{	Instantiate(effect01, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (40,40,20,20), GUIContent ("", "Slash02")))
	{	Instantiate(effect02, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (70,40,20,20), GUIContent ("", "Slash03")))
	{	Instantiate(effect03, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (100,40,20,20), GUIContent ("", "Slash04")))
	{	Instantiate(effect04, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (130,40,20,20), GUIContent ("", "Slash05")))
	{	Instantiate(effect05, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (160,40,20,20), GUIContent ("", "Slash06")))
	{	Instantiate(effect06, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (190,40,20,20), GUIContent ("", "Slash07")))
	{	Instantiate(effect07, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	
	if(GUI.Button (Rect (10,70,20,20), GUIContent ("", "Slash08")))
	{	Instantiate(effect08, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (40,70,20,20), GUIContent ("", "Slash09")))
	{	Instantiate(effect09, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (70,70,20,20), GUIContent ("", "Slash10")))
	{	Instantiate(effect10, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (100,70,20,20), GUIContent ("", "Slash11")))
	{	Instantiate(effect11, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}
	if(GUI.Button (Rect (130,70,20,20), GUIContent ("", "Slash12")))
	{	Instantiate(effect12, new Vector3(0, 1.2, -1.4), Quaternion.Euler(0, 0, 0));	
		zombie.GetComponent.<Animation>().Play("attack");
		zombie.GetComponent.<Animation>().PlayQueued("idle");
	}

	
	GUI.Label (Rect (10,Screen.height-30,300,20), GUI.tooltip);
	
}
