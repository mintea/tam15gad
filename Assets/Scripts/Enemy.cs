using UnityEngine;
using System.Collections;

public class Enemy : Unit {
	
	public GameObject drop;
	public Transform target;
	public int collideDamage = 1;
	public float dropRate;
	
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		Target ();
		
		if (moveDir != Vector3.zero){
			
			RotateUnit();
			MoveUnit();
		}
	}
	
	protected void Target() {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag("Player").transform; // set player as the target
		}
		else {
			SetDirection (
				target.position.x - _transform.position.x,
				target.position.z - _transform.position.z
			);
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			KillUnit(); // suicide
			collider.GetComponent<Player>().AdjustHealth(-collideDamage);
		}
	}
	
    void OnDestroy () {
        if (Random.value <= dropRate) {
			Instantiate(drop,_transform.position, Quaternion.identity);
		}
    }


}
