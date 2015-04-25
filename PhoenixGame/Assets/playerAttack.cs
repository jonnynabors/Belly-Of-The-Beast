using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public float attackSpeed = 0.5f;
	public Animator anim;
	float timer;
	public PlayerStamina playerStamina;
	public int currentStamina;
	public GameObject SlashEffect;
	public float startDelay = 0.5f;
	public AudioClip attackSound;
	public AudioSource audio;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
		playerStamina = GetComponent<PlayerStamina> ();
		currentStamina = playerStamina.currentStamina;
		SlashEffect = GameObject.FindGameObjectWithTag ("Slash");
		audio = GetComponent<AudioSource> ();
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
		audio.PlayOneShot (attackSound);
		SlashEffect.GetComponent<ParticleSystem> ().startDelay = startDelay;
		SlashEffect.GetComponent<ParticleSystem> ().Play ();
	}
}