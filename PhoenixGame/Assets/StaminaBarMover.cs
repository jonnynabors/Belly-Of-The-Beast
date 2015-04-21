using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StaminaBarMover : MonoBehaviour {
	
	public Sprite[] sprites;
	public Image staminaBar;
	public GameObject player;
	public PlayerStamina playerStaminaScript;
	public int stam;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStaminaScript = player.GetComponent<PlayerStamina> ();
		stam = playerStaminaScript.currentStamina;
	}
	
	public void updateStaminaBar()
	{
		stam = playerStaminaScript.currentStamina;

		if (stam > 1)
			staminaBar.enabled = true;

		if (stam == 150)
			staminaBar.sprite = sprites[0];
		
		else if (stam >= 135)
			staminaBar.sprite = sprites[1];
		
		else if (stam >= 120)
			staminaBar.sprite = sprites[2];
		
		else if (stam >= 105)
			staminaBar.sprite = sprites[3];
		
		else if (stam >= 90)
			staminaBar.sprite = sprites[4];
		
		else if (stam >= 75)
			staminaBar.sprite = sprites[5];
		
		else if (stam >= 60)
			staminaBar.sprite = sprites[6];
		
		else if (stam >= 45)
			staminaBar.sprite = sprites[7];
		
		else if (stam >= 30)
			staminaBar.sprite = sprites[8];
		
		else if (stam >= 15)
			staminaBar.sprite = sprites[9];
		
		else if (stam <= 0)
			staminaBar.enabled = false;
	}
}