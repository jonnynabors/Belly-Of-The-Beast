using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarMover : MonoBehaviour {

	public Sprite[] sprites;
	public Image healthBar;
	public GameObject player;
	public PlayerHealth playerHealthScript;
	public int hp;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealthScript = player.GetComponent<PlayerHealth> ();
	}

	public void updateHealthBar()
	{
		hp = playerHealthScript.currentHealth;

		if (hp == 200)
			healthBar.sprite = sprites[0];
		
		else if (hp >= 180)
			healthBar.sprite = sprites[1];

		else if (hp >= 160)
			healthBar.sprite = sprites[2];

		else if (hp >= 140)
			healthBar.sprite = sprites[3];

		else if (hp >= 120)
			healthBar.sprite = sprites[4];

		else if (hp >= 100)
			healthBar.sprite = sprites[5];

		else if (hp >= 80)
			healthBar.sprite = sprites[6];

		else if (hp >= 60)
			healthBar.sprite = sprites[7];

		else if (hp >= 40)
			healthBar.sprite = sprites[8];

		else if (hp >= 20)
			healthBar.sprite = sprites[9];

		else if (hp == 0)
			healthBar.enabled = false;
	}
}
