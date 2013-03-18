using UnityEngine;
using System.Collections;

public class enemyScript : MonoBehaviour 
{
	
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	public int maxDistance;
	public Transform enemyDeathAnimation;
	
	private Transform myTransform;
	// Use this for initialization
	
	void Awake()
	{
		myTransform = transform;
	}
	
	void Start () 
	{
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		
		target = go.transform;
		maxDistance = 2;
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
	
	void OnTriggerEnter(Collider playerProjectile)
	{
		if (playerProjectile.gameObject.tag == "playerProjectile")
		{
			Transform tempExplosion;
			
			tempExplosion = Instantiate (enemyDeathAnimation, transform.position, transform.rotation) as Transform;
			Destroy (gameObject);
		}
	}
}
