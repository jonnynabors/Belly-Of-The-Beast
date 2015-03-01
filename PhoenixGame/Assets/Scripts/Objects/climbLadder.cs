/*using UnityEngine;
using System.Collections;

public class climbLadder : MonoBehaviour {

	public ThirdPersonController charController;
	public bool inside = false;
	public float charHeight = 0;
	public Transform charTransform;

	// Use this for initialization
	void Start () {
		charController = GetComponent (ThirdPersonController);
	}

	void OnTriggerEnter(Collider Col)
	{
		if(Col.gameObject.tag == "Ladder")
		{
			charController.enabled = false;
			inside = !inside;
		}
	}

	void OnTriggerExit(Collider Col)
	{
		if(Col.gameObject.tag == "Ladder")
		{
			charController.enabled = true;
			inside = !inside;
		}
	}



	// Update is called once per frame
	void Update () {
		if (inside == true && Input.GetKey ("w")) {
			charTransform.transform.position += Vector3.up/charHeight;	
			
		}
	}
}
*/