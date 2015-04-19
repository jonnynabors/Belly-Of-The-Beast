using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public float attackSpeed = 0.15f;
	public Animator anim;
	float timer;
	public PlayerStamina playerStamina;
	public int currentStamina;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		playerStamina = GetComponent<PlayerStamina> ();
		currentStamina = playerStamina.currentStamina;
	}

	
	void Update ()
	{
		currentStamina = playerStamina.currentStamina;
		timer += Time.deltaTime;
		
		if (Input.GetMouseButtonDown (0) && timer >= attackSpeed && Time.timeScale != 0 && currentStamina >= 30) {
			attack ();
		}

	}
	
	
	public void DisableEffects ()
	{

	}
	
	
	void attack ()
	{
		anim.SetTrigger ("isAttack");
	}
}