using UnityEngine;
using System.Collections;

public class Fire : Projectile {

	// Use this for initialization
	void Start () {
		damage = 1;
		piercing = 100;
		moveSpeed = 25;
		transform.localScale *= 7;
	}
	
	// Update is called once per frame
	void Update () {
		Move( 5f );
	}
}
