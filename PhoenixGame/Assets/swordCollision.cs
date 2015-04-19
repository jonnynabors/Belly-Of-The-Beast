﻿using UnityEngine;
using System.Collections;

public class swordCollision : MonoBehaviour {

	public int attackDamage = 50;
	public Animator anim;
	public Collider player;
	public PlayerStamina playerStamina;
	// Use this forinitialization
	void Start () {
		anim = GetComponentInParent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
		Physics.IgnoreCollision(GetComponent<Collider>(), player);
		playerStamina = GetComponent<PlayerStamina> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider collision) 
	{
		if(collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent <Animator>().SetBool("EnemyTrigger",true);
			EnemyHealth enemyHealth = collision.transform.GetComponent <EnemyHealth> ();
			if(enemyHealth != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			{
				enemyHealth.EnemyTakeDamage (attackDamage);
			}
		}
	}
}
