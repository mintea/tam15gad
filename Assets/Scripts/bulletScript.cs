using UnityEngine;
using System.Collections;

public class bulletScript : MonoBehaviour {
	public int boundsX;
	public int boundsZ;
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
			Destroy (gameObject);
		}
	}
}
