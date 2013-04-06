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
	
	static int posCount = 10;
	public Vector3[] positions = new Vector3[posCount];

	
	// Use this for initialization
	void Start () {
		_transform = transform;
		for (int i = 0; i < posCount; ++i) {
			positions[i].x = 8*Mathf.Cos (i*2*Mathf.PI/posCount);
			positions[i].z = 8*Mathf.Sin (i*2*Mathf.PI/posCount);
		}
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
//				isDunking = true;
			}
			
		}
		if (target == _transform) target = null;
//		if (moveDir != Vector3.zero && target != null){
		if (isDunking) {
			// Target( "Player" );
			RotateUnit();
			// MoveUnit();
			// Dunk();
			// Pass();
			// isDunking = false;
		} else {
			MoveToPos(curCirclePos);
			RotateUnit();
			if (moveDir != Vector3.zero)
//			Debug.Log(Vector3.Distance (_transform.position, moveDir));
//			if (Vector3.Distance (_transform.position, moveDir) >= 7)
				MoveUnit();
		}
	}
	
	public void MoveToPos(int pos) {
		SetDirection (
			positions[pos%posCount].x - _transform.position.x,
			positions[pos%posCount].z - _transform.position.z
		);
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
			if (target != null) {
				SetDirection (
					target.position.x - _transform.position.x,
					target.position.z - _transform.position.z
				);
			}
			
			RotateUnit();
			
			GameObject ball = Instantiate(ballPrefab, _transform.position + _transform.forward*2, _transform.rotation) as GameObject;
			Basketball bball = ball.GetComponent<Basketball>();
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
