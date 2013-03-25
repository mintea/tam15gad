using UnityEngine;
using System.Collections;

public class SplitShotScript : MonoBehaviour {
	public bulletScript bullet;
	public float bulletSpeed;
	public int boundsX;
	public int boundsZ;
	
	// Use this for initialization
	void Start () 
	{
		boundsX = 20;
		boundsZ = 20;
		bulletSpeed = 6;
		transform.Rotate( 0, -45, 0 );
		for( int i = 0; i < 5; ++i )
		{
			transform.Rotate( 0, 15, 0 );
//			Quaternion q.Set( 0, 15*i, 0 );
//			transform.rotation = ( 0, 15*i, 0, 0 );
			Instantiate( bullet, transform.position, transform.rotation );
		}
			
		Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		//amount to move bullet
		float amtToMove = bulletSpeed * Time.deltaTime;
		
		//Destroys bullets if it goes out of bounds
		for( int i = 0; i < 5; ++i )
		{
			if (Mathf.Abs(transform.position.z) > boundsZ || Mathf.Abs (transform.position.x) > boundsX)
			{
				Destroy(gameObject);
			}
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
