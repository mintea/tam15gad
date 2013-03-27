using UnityEngine;
using System.Collections;
using System; // for Enum

public class Weapon : MonoBehaviour {
	
	public GameObject projectile;
	
	public Vector3 direction = Vector3.zero;		// the weapon's direction
	
	public float coolDown = 0.2f; // time between shots
	
	protected Transform _transform;
	private float _nextShot; // time off cooldown
	
	// Use this for initialization
	void Awake () {
		_transform = transform;
	}
	
	public void Shoot(WeaponName weapon) {
//		GameObject go = Instantiate(bullets[(int)weapon], _transform.position + _transform.forward, _transform.rotation) as GameObject;
		if (Time.time > _nextShot) {
			GameObject go = Instantiate(projectile, _transform.position + _transform.forward, _transform.rotation) as GameObject;
			
			switch(weapon) {
			case WeaponName.Laser:
//				coolDown = 0.5f;
				go.AddComponent<Laser>();
				go.renderer.material.color = Color.red;
				break;
			case WeaponName.Bullet:
			case WeaponName.Split:
			default:
//				coolDown = 0.2f;
				go.AddComponent<Bullet>();
				break;
			}
			
			if (_transform.parent.tag == "Player") {
					go.GetComponent<Projectile>().isPlayer = true;
			}
			
			if (weapon == WeaponName.Split) {	
				float spreadAngle = 90;
				int bullets=5;
//				coolDown = 0.1f;
				
				// set first shot
				go.renderer.material.color = Color.blue;
				go.transform.Rotate( 0, -spreadAngle/2, 0 );
				
				for( int i = 0; i < bullets; ++i ) {
					Instantiate(go);
					go.transform.Rotate( 0, spreadAngle/bullets, 0 );
				}
				
			}
			
			_nextShot = Time.time + coolDown;
		}
	}
	
	public void SetDirection(float x, float z) {
		direction.x = x;
		direction.z = z;
		if (direction != Vector3.zero) {
			_transform.rotation = Quaternion.LookRotation(direction);
		}
	}
	
}


public enum WeaponName {
	Bullet,
	Split,
	Laser,
}