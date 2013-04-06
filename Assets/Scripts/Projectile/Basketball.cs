using UnityEngine;
using System.Collections;

public class Basketball : MonoBehaviour {
	public Transform target;
	public float moveSpeed;
	public int   damage;
	public int   piercing;
	public bool  isPlayer;
	public float moveAmount;
	public Vector3 moveDir = Vector3.zero;
	
	private Transform _transform;
	private Vector3 _startPos;	// for debugging
	private float _killZone;
	
	public Color[] colorChoices = {
		Color.red,
		new Color(1,0.4f,0), // orange
		Color.yellow,
		Color.green,
		Color.blue,
		Color.magenta,
		Color.cyan,
		Color.white,
		Color.gray
	};
	
	
	// Use this for initialization
	void Start () {
		_transform = transform;
		_startPos = _transform.position;
		_killZone = Camera.main.GetComponent<GameMaster>().viewWidth * 2;
	}
	
	// Update is called once per frame
	void Update () {
		Move();
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
	
	public void Move() {
		moveAmount = moveSpeed * Time.deltaTime;
		_transform.Translate (Vector3.forward * moveAmount);
		Debug.DrawLine (_startPos, _transform.position, Color.grey);
		
		//Destroys bullets if it goes out of bounds
//		if (Vector3.Distance (_transform.position, Vector3.zero) > _killZone)
//		{
//			Destroy(gameObject);
//		}
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
	
	protected void SetDirection(float x, float z) {
		moveDir.x = x;
		moveDir.z = z;
	}
}
