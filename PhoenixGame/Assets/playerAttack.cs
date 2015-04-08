using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public float attackSpeed = 0.15f;
	public Animator anim;
	float timer;

	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	

	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if (Input.GetMouseButtonDown (0) && timer >= attackSpeed && Time.timeScale != 0) {
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