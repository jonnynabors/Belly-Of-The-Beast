using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public float attackSpeed = 0.5f;
	public Animator anim;
	float timer;
	public PlayerStamina playerStamina;
	public int currentStamina;
	public GameObject SlashEffect;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		playerStamina = GetComponent<PlayerStamina> ();
		currentStamina = playerStamina.currentStamina;
		SlashEffect = GameObject.FindGameObjectWithTag ("Slash");
	}

	
	void Update ()
	{
		currentStamina = playerStamina.currentStamina;
		timer += Time.deltaTime;
		
		if (Input.GetMouseButtonDown (0) && (timer > attackSpeed) && currentStamina > 0) {
			attack ();
			timer = 0f;
		}

	}
	
	
	public void DisableEffects ()
	{

	}
	
	
	void attack ()
	{
		anim.SetTrigger ("isAttack");
		SlashEffect.GetComponent<ParticleSystem> ().Play ();
	}
}