using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
	public float moveSpeed;
	public int   damage;
	public int   piercing;
	public bool  isPlayer;
	public float moveAmount;
	private Transform _transform;
	public Color color;
	public Vector3 StartPos;
	public float killZone;
	
	public Color[] colorChoices = {
		Color.red,
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
		color = _transform.renderer.material.color;
		StartPos = _transform.position;
		killZone = Camera.main.GetComponent<GameMaster>().viewWidth;
		InvokeRepeating("ChangeColor",0,0.25f);
	}
	
	public void Move() {
		moveAmount = moveSpeed * Time.deltaTime;
		_transform.Translate (Vector3.forward * moveAmount);
		Debug.DrawLine (StartPos, _transform.position, Color.grey);
//		ChangeColor();
		//Destroys bullets if it goes out of bounds
//		if (Mathf.Abs(Vector3.Distance(_transform.position.z) > boundsZ; || Mathf.Abs (_transform.position.x) > boundsX)
		if (Vector3.Distance (_transform.position, Vector3.zero) > killZone)
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		
		Unit unit = collider.gameObject.GetComponent<Unit>();
		
		if (unit != null) {
			if ((isPlayer && unit.tag=="Enemy")||(!isPlayer && unit.tag=="Player")) {
				unit.AdjustHealth(-damage);
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
