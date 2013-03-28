using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour 
{
	
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	public Transform enemyDeathAnimation;
	public int health;
	public bool Flagged;
	
	private Transform myTransform;
	// Use this for initialization
	
	void Awake()
	{
		myTransform = transform;
	}
	
	void Start () 
	{
		moveSpeed = 5;
		rotationSpeed = 10;
		health = 100;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		target = go.transform;
		maxDistance = 2; // not used yet
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Looks at target
		myTransform.rotation = Quaternion.Slerp (myTransform.rotation, 
			Quaternion.LookRotation (target.position - myTransform.position),rotationSpeed * Time.deltaTime);
		
		//Move towards target
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
		
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "playerProjectile")
		{
			health--;
			Debug.Log( health );
			if (health <= 0){
				Instantiate (enemyDeathAnimation, transform.position, transform.rotation);
				Destroy (gameObject);
			}
			Destroy( collider.gameObject );
		}
		else if (collider.gameObject.tag == "Player")
		{
			Instantiate (enemyDeathAnimation, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

}
