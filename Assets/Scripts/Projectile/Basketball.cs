using UnityEngine;
using System.Collections;
using System;

public class Basketball : Unit {
	
	public GameObject drop;
	public Transform target;
	public int damage = 0;
	public float dropRate;
	
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		
		Target ( "BasketBoss" );
		
		if (moveDir != Vector3.zero){
			
			RotateUnit();
			MoveUnit();
		}
	}
	
	protected void Target( string unitTarget ) {
		if (target == null) {
			target = GameObject.FindGameObjectWithTag( unitTarget ).transform; // set player as the target
		}
		else {
			SetDirection (
				target.position.x - _transform.position.x,
				target.position.z - _transform.position.z
			);
		}
	}
	
	void OnTriggerEnter( Collider collider )
	{
		if (collider.gameObject.tag == "Player")
		{
			collider.GetComponent<Player>().AdjustHealth( -damage );
			Target( "BasketBoss" );
		}
		else if( collider.gameObject.tag == "BasketBoss" )
		{
			collider.GetComponent<BasketBoss>().getPos();
			Destroy( gameObject );
		}
	}

	public void SetTarget( Transform t) {
		target = t;
	}	
//	void OnTriggerEnter(Collider collider)
//	{
//		if (collider.gameObject.tag == "Player")
//		{
//			KillUnit(); // suicide
//			collider.GetComponent<Player>().AdjustHealth(-collideDamage);
//		}
//	}
	
}
