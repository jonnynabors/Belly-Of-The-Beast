using UnityEngine;
using System.Collections;

public class swordCollision : MonoBehaviour {

	public int attackDamage = 50;
	public Animator anim;
	public Collider player;
	public PlayerStamina playerStamina;
	public bool isInBody;
	public AudioClip waterSlashSound;
	public AudioClip woodSlashSound;
	public AudioClip grassSlashSound;
	public AudioClip humaniodSlashSound;
	public AudioClip stoneSlashSound;
	public AudioSource audio;
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
	void OnCollisionEnter(Collision collision) 
	{
		if(collision.gameObject.tag == "Enemy")
		{
			collision.gameObject.GetComponent <Animator>().SetBool("EnemyTrigger",true);
			EnemyHealth enemyHealth = collision.transform.GetComponent <EnemyHealth> ();
			if(enemyHealth != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !isInBody)
			{
				isInBody = true;
				enemyHealth.EnemyTakeDamage (attackDamage);
				humanoidSound ();
			}
		}
		if(collision.gameObject.tag == "Ladder"&& anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
			woodSound ();
		}
		if(collision.gameObject.tag == "Boulder"&& anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
			stoneSound ();
		}
		if(collision.gameObject.tag == "Stone"&& anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
			stoneSound ();
		}
		if(collision.gameObject.tag == "Grass"&& anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
			grassSound ();
		}
		if(collision.gameObject.tag == "Water"&& anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
			waterSound ();
		}
	}

	void OnCollisionExit(Collision collision)
	{
		isInBody = false;
	}
	void stoneSound(){
		audio.PlayOneShot(stoneSlashSound);		
	}
	void woodSound(){
			audio.PlayOneShot(woodSlashSound);		
	}
	void waterSound(){
		audio.PlayOneShot(waterSlashSound);		
	}
	void grassSound(){
		audio.PlayOneShot(grassSlashSound);		
	}
	void humanoidSound(){
		audio.PlayOneShot(humaniodSlashSound);		
	}
}
