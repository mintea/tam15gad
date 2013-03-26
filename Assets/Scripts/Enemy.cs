using UnityEngine;
using System.Collections;

public class Enemy : Unit {
	
	public Transform target;
	
	void Awake () {
//		target = GameObject.FindGameObjectWithTag("Player").transform; // set player as the target
	}
	
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (target != null)
			SetDirection (
				target.position.x - _transform.position.x,
				target.position.z - _transform.position.z
			);
		
		else {
			target = GameObject.FindGameObjectWithTag("Player").transform; // set player as the target
		}
		
		if (direction != Vector3.zero){
			
			RotateUnit();
			MoveUnit();
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			KillUnit(); // suicide
			collider.GetComponent<Player>().AdjustHealth(-1);
		}
	}

}
