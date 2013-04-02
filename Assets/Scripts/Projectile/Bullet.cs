using UnityEngine;
using System.Collections;

public class Bullet : Projectile {
	void Start() {
		damage = 1;
		moveSpeed = 10;
		piercing = 0;
	}
	
	void Update() {
		Move ();
	}
}
