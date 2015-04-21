using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {
	public MovieTexture movie;

	void Awake(){
	}


	void Start(){
	}

	void OnGUI(){
		movie.Play ();
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), movie, ScaleMode.StretchToFill, false, 0.0f);

	}


}
