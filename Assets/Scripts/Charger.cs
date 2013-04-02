using UnityEngine;
using System.Collections;

public class Charger : Enemy {
	
	void Start () {
		_transform = transform;
		StartCoroutine(Charge());
	}
	
	// Update is called once per frame
	void Update () {
		Target ( "Player" );
		
		if (moveDir != Vector3.zero){
			
			RotateUnit();
			MoveUnit();
		}

	}
	
	IEnumerator Charge() {
		renderer.material.color = Color.magenta;
		yield return new WaitForSeconds(Random.Range (.5f,2f));
		renderer.material.color = Color.red;
		rotSpeed = 20;
		moveSpeed *= 2;
	}

}
