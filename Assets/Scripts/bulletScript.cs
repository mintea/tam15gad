using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	
	public float bulletSpeed;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//amount to move bullet
		float amtToMove = bulletSpeed * Time.deltaTime;
		
		transform.Translate (Vector3.forward * amtToMove);
		
		//Destroys bullets if it goes off screen
		//**REMINDER**
		//Please code for all edges once we get directional bullets
		if (transform.position.z >= 7)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			Destroy (gameObject);
		}
	}
}
