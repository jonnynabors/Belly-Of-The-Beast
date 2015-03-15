using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class videoController : MonoBehaviour {
	
	public Button reso1, reso2, reso3, reso4;
	public Button quality1, quality2, quality3, quality4, quality5, quality6;
	public Scrollbar brightness;
	public Image brightnessCanvas;

	// Use this for initialization
	void Start () {
		reso1 = reso1.GetComponent<Button> ();
		reso2 = reso1.GetComponent<Button> ();
		reso3 = reso1.GetComponent<Button> ();
		reso4 = reso1.GetComponent<Button> ();
		quality1 = quality1.GetComponent<Button> ();
		quality2 = quality1.GetComponent<Button> ();
		quality3 = quality1.GetComponent<Button> ();
		quality4 = quality1.GetComponent<Button> ();
		quality5 = quality1.GetComponent<Button> ();
		quality6 = quality1.GetComponent<Button> ();
		brightness = brightness.GetComponent<Scrollbar> ();
		brightnessCanvas = brightnessCanvas.GetComponent<Image> ();
	}

	void Update(){

		OnValueChange ();
		
	}

	public void reso1Press(){
		Screen.SetResolution (1920, 1080, true);
	}
	public void reso2Press(){
		Screen.SetResolution (1600, 900, true);
	}
	public void reso3Press(){
		Screen.SetResolution (1280, 720, true);
	}
	public void reso4Press(){
		Screen.SetResolution (960, 540, true);
	}

	public void quality1Press(){
		QualitySettings.SetQualityLevel (0, true);
	}
	public void quality2Press(){
		QualitySettings.SetQualityLevel (1, true);
	}
	public void quality3Press(){
		QualitySettings.SetQualityLevel (2, true);
	}
	public void quality4Press(){
		QualitySettings.SetQualityLevel (3, true);
	}
	public void quality5Press(){
		QualitySettings.SetQualityLevel (4, true);
	}
	public void quality6Press(){
		QualitySettings.SetQualityLevel (5, true);
	}

	void OnValueChange()
	{
		if (brightness.value > .95) {
			brightnessCanvas.color = new Color32(0,0,0,0);
		}
		if (brightness.value <= .95) {
			brightnessCanvas.color = new Color32(0,0,0,20);
		}
		if (brightness.value <= .83) {
			brightnessCanvas.color = new Color32(0,0,0,40);
		}
		if (brightness.value <= .72) {
			brightnessCanvas.color = new Color32(0,0,0,60);
		}
		if (brightness.value <= .61) {
			brightnessCanvas.color = new Color32(0,0,0,80);
		}
		if (brightness.value <= .5) {
			brightnessCanvas.color = new Color32(0,0,0,100);
		}
		if (brightness.value <= .38) {
			brightnessCanvas.color = new Color32(0,0,0,120);
		}
		if (brightness.value <= .27) {
			brightnessCanvas.color = new Color32(0,0,0,140);
		}
		if (brightness.value <= .16) {
			brightnessCanvas.color = new Color32(0,0,0,160);
		}
		if (brightness.value <= .05) {
			brightnessCanvas.color = new Color32(0,0,0,180);
		}

	}
}
