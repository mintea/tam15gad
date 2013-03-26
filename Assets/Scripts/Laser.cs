using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {
	public int boundsX;
	public int boundsZ;
	public float bulletSpeed;
	
	// Use this for initialization
	void Start () 
	{
		boundsX = 20;
		boundsZ = 20;
		bulletSpeed = 8;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//amount to move bullet
		float amtToMove = bulletSpeed * Time.deltaTime;
		
//		Laser lookin shot
		transform.Translate (Vector3.forward * amtToMove * 2);
		transform.localScale += (Vector3.forward * amtToMove * 2);
		
		//Destroys bullets if it goes out of bounds
		if (Mathf.Abs(transform.position.z) > boundsZ || Mathf.Abs (transform.position.x) > boundsX)
		{
			Destroy(gameObject);
		}
	}
	
	
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			enemy.gameObject.GetComponent<enemyScript>().Flagged = true;
		}
	}
}
