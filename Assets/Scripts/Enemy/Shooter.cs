using UnityEngine;
using System.Collections;

public class Shooter : Enemy {
	public Weapon enemyWeap;
	
	// Use this for initialization
	void Start () {
		_transform = transform;
		curWeapon = 0;
		enemyWeap = _transform.GetComponentInChildren<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
		Target( "Player" );
		
		if( moveDir != Vector3.zero ){
			RotateUnit();
			MoveUnit();
		}
		
		if( enemyWeap == null ){
			enemyWeap = _transform.GetComponentInChildren<Weapon>();
		}
		else{
			enemyWeap.SetDirection( 1f, 1f );			//shoots at 45 degrees
			enemyWeap.Shoot((WeaponName)curWeapon);
		}
	}
}
