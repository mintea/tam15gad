using UnityEngine;
using System.Collections;

public class Player : Unit {
	public Weapon weapon;
	public bool isBuff;
	public float buffTime;
	
	// Use this for initialization
	void Start () {
		invincibleTime = 2;
		curHealth = maxHealth;
		moveSpeed = 6;
		rotSpeed = 100;
		_transform = transform;
		weapon = _transform.GetComponentInChildren<Weapon>();
		curWeapon = 0;
	}
	void Awake() {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// get joystick direction
		SetDirection (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"));
		
		// rotate and move
		if (moveDir != Vector3.zero){
			RotateUnit();
			MoveUnit();
		}
		
		//WeaponSwitch
		if( Input.GetKeyDown(KeyCode.Q) )
		{
			curWeapon--;
			if( curWeapon < 0 )
				curWeapon = 3;
		}
		else if( Input.GetKeyDown(KeyCode.E) )
		{
			curWeapon++;
			if( curWeapon > 3 )
				curWeapon = 0;
		}
		//End of WeaponSwitch
		
		
		// reset buff if not buff
//		if (isBuff && Time.time > buffTime) {
//			curWeapon = 0;
//			isBuff = false;
//		}
		
		if (weapon == null) {
			weapon = _transform.GetComponentInChildren<Weapon>();
		}
		else {
			weapon.SetDirection(Mathf.Round (Input.GetAxis ("HorizontalFire")),Mathf.Round(Input.GetAxis ("VerticalFire")));
			// shoot if using joystick
			if (weapon.direction != Vector3.zero) {
				weapon.Shoot((WeaponName)curWeapon);
			}
		}
		
	}
	
	public void SetBuff(int weap, float time) {
		isBuff = true;
		curWeapon = weap;
		buffTime = Time.time + time;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy")
		{
			_transform.position += (_transform.position - collider.transform.position)/2;
		}
	}

}
