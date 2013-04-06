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
			enemyWeap.SetLocalDirection( Mathf.PingPong(Time.time*2.5f, 2)-1, 1f );
			enemyWeap.Shoot((WeaponName)curWeapon);
		}
	}
}
