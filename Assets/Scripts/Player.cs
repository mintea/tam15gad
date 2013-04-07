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
		curWeapon = heldWeapon = 0;
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
		//Still need to add in GamePad controls
		if( Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) )
		{
			int temp = curWeapon;
			curWeapon = heldWeapon;
			heldWeapon = temp;
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
	
	public void SetBuff( int weap )
	{
		curWeapon = weap;
	}
	
	public void DropWeap()
	{
		curWeapon = 0;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "BasketBoss")
		{
			_transform.position += (_transform.position - collider.transform.position)/2;
			curWeapon = 0;
		}
	}

}
