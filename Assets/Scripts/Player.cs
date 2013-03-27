using UnityEngine;
using System.Collections;

public class Player : Unit {
	public Weapon weapon;
	
	// Use this for initialization
	void Start () {
		rotSpeed = 100;
		_transform = transform;
		weapon = _transform.GetComponentInChildren<Weapon>();
		
	}
	void Awake() {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// get joystick direction
		SetDirection (Input.GetAxis ("Horizontal"),Input.GetAxis ("Vertical"));
		
		// rotate and move
		if (direction != Vector3.zero){
			RotateUnit();
			MoveUnit();
		}
		
		if (Input.GetKeyDown(KeyCode.JoystickButton11)){
			ChangeWeapon();
		}
		
		if (weapon == null) {
			weapon = _transform.GetComponentInChildren<Weapon>();
		}
		else {
			weapon.SetDirection(Input.GetAxis ("HorizontalFire"),Input.GetAxis ("VerticalFire"));
			// shoot if using joystick
			if (weapon.direction != Vector3.zero) {
				weapon.Shoot((WeaponName)curWeapon);
			}
		}
		
	}
	
	void ChangeWeapon() {
		curWeapon = (curWeapon+1) % 3;
	}
}
