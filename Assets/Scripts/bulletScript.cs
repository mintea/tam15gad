using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	public int boundsX;
	public int boundsZ;
	public float bulletSpeed;
	
	// Use this for initialization
	void Start () 
	{
		boundsX = 20;
		boundsZ = 20;
		bulletSpeed = 6;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//amount to move bullet
		float amtToMove = bulletSpeed * Time.deltaTime;
		
		transform.Translate (Vector3.forward * amtToMove);
		
//		Laser lookin shot
//		transform.Translate (Vector3.forward * amtToMove * 2);
//		transform.localScale += (Vector3.forward * amtToMove * 3);
		
		//Destroys bullets if it goes out of bounds
		if (Mathf.Abs(transform.position.z) > boundsZ || Mathf.Abs (transform.position.x) > boundsX)
		{
			Destroy(gameObject);
		}
	}
	
	/*
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			Destroy (gameObject);
		}
	}*/
}
