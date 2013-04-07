using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed;
	public int   damage;
	public int   piercing;
	public bool  isPlayer;
	public float moveAmount;
	
	private Transform _transform;
	private Vector3 _startPos;	// for debugging
	private float _killZone;
	private GameMaster _gm;
	
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
	
	
	
	void Awake() {
		_transform = transform;
		_startPos = _transform.position;
		_gm = Camera.main.GetComponent<GameMaster>();
		_killZone = _gm.viewWidth * 2;
//		Debug.Log( _killZone );
//		InvokeRepeating("ChangeColor",0,0.25f);
	}
	
	public void Move() {
		moveAmount = moveSpeed * Time.deltaTime;
		_transform.Translate (Vector3.forward * moveAmount);
		Debug.DrawLine (_startPos, _transform.position, Color.grey);
		
		//Destroys bullets if it goes out of bounds
		if (Vector3.Distance (_transform.position, Vector3.zero) > _killZone)
		{
			Destroy(gameObject);
		}
	}
	
	public void Move( float killZone ) {
		moveAmount = moveSpeed * Time.deltaTime;
		_transform.Translate (Vector3.forward * moveAmount);
		Debug.DrawLine (_startPos, _transform.position, Color.grey);
		
		//Destroys bullets if it goes out of bounds
		if (Vector3.Distance ( _transform.position, _startPos ) > killZone)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		
		Unit unit = collider.gameObject.GetComponent<Unit>();
		
		if (unit != null) {
			if ((isPlayer && unit.tag=="Enemy")||(!isPlayer && unit.tag=="Player")||(!isPlayer && unit.tag=="BasketBoss")) {
				unit.AdjustHealth(-damage);
				if (isPlayer || unit == null) {
					_gm.IncrementKillCount( collider );
				}
				if (!isPlayer && unit.tag == "Player"){
					Player player = collider.gameObject.GetComponent<Player>();
					player.DropWeap();
				}
				if (--piercing < 0) {
					Destroy(gameObject);
				}
			}
		}
	}
	
	void ChangeColor() {
		_transform.renderer.material.color = colorChoices[Random.Range(0, colorChoices.Length)];
	}
}
