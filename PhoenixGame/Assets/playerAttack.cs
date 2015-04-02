using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public int attackDamage = 20;
	public float attackSpeed = 0.15f;
	public float attackRange = 5f;

	private bool enemyInRange;
	public Animator anim;
	public Collider swordCollider;

	
	
	float timer;
	Ray shootRay;
	RaycastHit shootHit;
	int shootableMask;
	ParticleSystem gunParticles;
	LineRenderer gunLine;
	AudioSource gunAudio;
	Light gunLight;
	float effectsDisplayTime = 0.2f;
	
	
	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	

	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(Input.GetMouseButton (0) && timer >= attackSpeed && Time.timeScale != 0)
		{
			attack ();
		}
		
		if(timer >= attackSpeed * effectsDisplayTime)
		{
			DisableEffects ();
		}

	}
	
	
	public void DisableEffects ()
	{

	}
	
	
	void attack ()
	{
		anim.SetTrigger ("isAttack");

//		timer = 0f;
//
//		gunLine.enabled = true;
//
//		gunLine.SetPosition (0, transform.position);
//		
//		shootRay.origin = transform.position;
//		shootRay.direction = transform.forward;
//		
//		if(Physics.Raycast (shootRay, out shootHit, attackRange, shootableMask))
//		{
//			EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
//			if(enemyHealth != null)
//			{
//				EnemyHealth.EnemyTakeDamage (attackDamage);
//			}
//			gunLine.SetPosition (1, shootHit.point);
//		}
//		else
//		{
//			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * attackRange);
//		}
	}
}