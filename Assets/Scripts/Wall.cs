using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	
	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player")
		{
			collider.GetComponent<Player>().KillUnit();
		}
	}
	
	void OnTriggerExit(Collider collider) {
		if (collider.gameObject.tag == "Projectile")
		{
			Destroy(collider.gameObject);
		}
	}
}
