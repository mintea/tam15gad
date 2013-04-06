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
			if( UnityEngine.Random.value <= .8f )
			{
				Target( "BasketBoss" );
				if (target != _transform) {
					ballPossession = false; // pass ball to another boss
					StartCoroutine("WaitPass");
//					Pass ();
				}
//				target = null;
			}
			else
			{
				Debug.Log("DUNK");
				Target( "Player" );
				target = null;
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
			GameObject[] targets = GameObject.FindGameObjectsWithTag( unitTarget );
			int targetIndex = Random.Range (0,targets.Length);
			Debug.Log (targetIndex);
			target = targets[targetIndex].transform; // set player as the target
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
	
	IEnumerator WaitPass() {
		yield return new WaitForSeconds(Random.Range (.5f,2f));
		Pass ();
	}
	
	//Pass the ball after dunking on that fool
	void Pass()
	{
//		Target( "BasketBoss" );
		if (target != _transform) {
			ballPossession = false; // pass ball to another boss
			RotateUnit();
			
			GameObject ball = Instantiate(ballPrefab, _transform.position + _transform.forward*2, _transform.rotation) as GameObject;
			Basketball bball = ball.GetComponent<Basketball>();
			bball.SetTarget (target);
			
			target = null;
			renderer.material.color = Color.red;
		}
		target = null;
	}
	
//	Target( "BasketBoss" );
//				if (target != _transform) {
//					ballPossession = false; // pass ball to another boss
//					Pass();
//					renderer.material.color = Color.red;
//				}
//				target = null;
}
