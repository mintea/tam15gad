using UnityEngine;
using System.Collections;

public class Laser : Projectile {
	
	void Start() {
		damage = 1;
		moveSpeed = 20;
		piercing = 100;
	}
	
	void Update() {
		Move();
		Stretch();
	}
	
	void Stretch() {
		transform.localScale += (Vector3.forward * moveAmount);
	}
}
