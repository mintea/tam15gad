using UnityEngine;
using System.Collections;

public class BasketBoss : Unit {
	public Transform target;
	Basketball ball;
	bool ballPossession;
	int damage;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if( ballPossession && target == null )
		{
			if( UnityEngine.Random.value <= .8f )
			{
				Target( "Basketboss" );
				ballPossession = false;
			}
			else
			{
				Target( "Player" );
			}
		}
		Move();
	}
		
	public void getPos()
	{
		ballPossession = true;
		renderer.material.color = Color.blue;
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
		}
	}
	
	//Pass the ball after dunking on that fool
	void Pass()
	{
		Instantiate( ball );
	}
}
