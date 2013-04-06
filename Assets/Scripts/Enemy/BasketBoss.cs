using UnityEngine;
using System.Collections;

public class BasketBoss : Unit {
	public Transform target;
	public GameObject ballPrefab;
	public bool ballPossession;
	int damage;
	public float nextTargetTime;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null && ballPossession) // ( Time.time > nextTargetTime )
		{
			if( UnityEngine.Random.value <= 1.8f )
			{
				Target( "BasketBoss" );
				if (target != _transform) {
					ballPossession = false; // pass ball to another boss
					RotateUnit();
					Pass();
					renderer.material.color = Color.red;
				}
				target = null;
			}
			else
			{
				Debug.Log("DUNK");
				Target( "Player" );
			}
			
		}
		if (target == _transform) target = null;
		if (moveDir != Vector3.zero && target != null){
			MoveUnit();
			RotateUnit();
		}
	}
		
	public void getPos()
	{
		ballPossession = true;
		renderer.material.color = Color.blue;
	}
	
	protected void Target( string unitTarget ) {
		Debug.Log (unitTarget);
		if (target == null) {
			int targetIndex = Random.Range (0,5);
			Debug.Log (targetIndex);
			target = GameObject.FindGameObjectsWithTag( unitTarget )[targetIndex].transform; // set player as the target
			if (target == _transform) target = null;
//		}
//		else {
			if (target != null) {
				SetDirection (
					target.position.x - _transform.position.x,
					target.position.z - _transform.position.z
				);
			}
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
//		Target( "BasketBoss" );
		RotateUnit();
		GameObject ball = Instantiate(ballPrefab, _transform.position + _transform.forward*2, _transform.rotation) as GameObject;
		Basketball bball = ball.GetComponent<Basketball>();
		bball.SetTarget (target);
		target = null;
	}
}
