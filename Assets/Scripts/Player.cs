using UnityEngine;
using System.Collections;

public class Player : Unit {
	
	// Use this for initialization
	void Start () {
		rotSpeed = 100;
		_transform = transform;
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
	}
}
