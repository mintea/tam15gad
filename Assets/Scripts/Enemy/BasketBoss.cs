using UnityEngine;
using System.Collections;

public class BasketBoss : Unit {
	public Transform target;
	public GameObject ballPrefab;
	public bool ballPossession;
	int damage;
	public float nextTargetTime;
	public int curCirclePos;
	bool isDunking;
	public int myIndex;
	
	static int posCount = 10;
	public Vector3[] positions = new Vector3[posCount];

	
	// Use this for initialization
	void Start () {
		_transform = transform;
		for (int i = 0; i < posCount; ++i) {
			positions[i].x = 8*Mathf.Cos (i*2*Mathf.PI/posCount);
			positions[i].z = 8*Mathf.Sin (i*2*Mathf.PI/posCount);
		}
		if (ballPossession) {
			renderer.material.color = Color.blue;
		} else {
			renderer.material.color = Color.red;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (target == null && ballPossession && !isDunking) // ( Time.time > nextTargetTime )
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
			else if (!isDunking)
			{
				Debug.Log("DUNK");
				Target( "Player" );
				isDunking = true;
			}
			
		}
//		if (target == _transform) target = null;
		if (isDunking) {
			Target( "Player" );
			MoveToPos ();
			RotateUnit();
			if (moveDir != Vector3.zero){
				MoveUnit();
				MoveUnit();
				MoveUnit();
			}
//			Dunk();
		} else {
			MoveToPos(curCirclePos);
			RotateUnit();
//			Debug.Log (Vector3.Distance (positions[curCirclePos], _transform.position));
			if (moveDir != Vector3.zero && Vector3.Distance (positions[curCirclePos], _transform.position) > 0.1f)
//			if (Vector3.Distance)
				MoveUnit();
		}
	}
	
	public void MoveToPos(int pos) {
		SetDirection (
			positions[pos%posCount].x - _transform.position.x,
			positions[pos%posCount].z - _transform.position.z
		);
	}
	public void MoveToPos() {
		SetDirection (
			target.position.x - _transform.position.x,
			target.position.z - _transform.position.z
		);
	}
	
	public void getPos()
	{
		ballPossession = true;
		renderer.material.color = Color.blue;
	}
	
	protected void Target( string unitTarget ) {
		if (target == null) {
			GameObject[] targets = GameObject.FindGameObjectsWithTag( unitTarget );
			int targetIndex = Random.Range (0,targets.Length);
			Debug.Log (unitTarget + ": " +targetIndex);
			target = targets[targetIndex].transform; // set player as the target
			if (target == _transform) target = null; // clear target if it's itself
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
			// dunk
			isDunking = false;
			target=null;
//			Pass ();
			ballPossession=false;
//			Pass();
//			StartCoroutine("WaitPass");
		}
	}
//	
	void OnTriggerExit( Collider collider )
	{
		if (collider.gameObject.tag == "Player")
		{
//			StartCoroutine("WaitPass");
			Pass ();
		}
	}
	
	IEnumerator WaitPass() {
		yield return new WaitForSeconds(Random.Range (0.5f,1.5f));
		Pass ();
	}
	
	//Pass the ball after dunking on that fool
	void Pass()
	{
		Debug.Log("PASS");
//		Target( "BasketBoss" );
		if (target != _transform) {
			if (target != null) {
				SetDirection (
					target.position.x - _transform.position.x,
					target.position.z - _transform.position.z
				);
			}
			
			RotateUnit();
			
			ballPossession = false; // pass ball to another boss
			GameObject ball = Instantiate(ballPrefab, _transform.position + _transform.forward*2, _transform.rotation) as GameObject;
			Basketball bball = ball.GetComponent<Basketball>();
			if (target != null && target.transform.tag == "BasketBoss")
				bball.SetTarget (target);
			
			target = null;
			renderer.material.color = Color.red;
		}
		target = null;
	}
	
	public void RotatePosition(int i) {
		if (i%2==0) {
			curCirclePos = (curCirclePos+posCount-1)%posCount;
		}
		else {
			curCirclePos = (curCirclePos+1)%posCount;
		}
	}
}
