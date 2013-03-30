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
		if (Time.time > _nextShot) {
			GameObject go = Instantiate(projectile, _transform.position + _transform.forward, _transform.rotation) as GameObject;
			
//			go.transform.Rotate( 0, UnityEngine.Random.Range (-3f,3f), 0 ); // random angle for shots
			
			switch(weapon) {
				case WeaponName.Bullet:
					go.AddComponent<Bullet>();
					go.renderer.material.color = new Color(1f,0.5f,0); // yellow
					coolDown = 0.2f;
					break;
					
				case WeaponName.Laser:
					go.AddComponent<Laser>();
					go.renderer.material.color = new Color(0,0.5f,1); // light blue
					coolDown = 0.5f;
					break;
					
				case WeaponName.SplitBullet:
					go.AddComponent<Bullet>();
					go.renderer.material.color = Color.red; // yellow
					coolDown = 0.4f;
					break;
				
				case WeaponName.SplitLaser:
					go.AddComponent<Laser>();
					coolDown = 1f;
					break;
	
				default:
					go.AddComponent<Bullet>();
					coolDown = 0.2f;
					break;
			}
			
			// set player flag
			if (_transform.parent.tag == "Player") {
				go.GetComponent<Projectile>().isPlayer = true;
			}
			
			if (weapon == WeaponName.SplitBullet|| weapon == WeaponName.SplitLaser) {	
				SpawnSplit(go, 45, 4);
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
	
	// This spawns the bullets for the split shot.
	// projectile is the projectile to clone 
	// angle is the spread angle
	// bullets is the number of bullets in this spread shot
	void SpawnSplit(GameObject projectile, float angle, int bullets) {
		
		projectile.transform.Rotate( 0, -angle/2, 0 );
		
		for( int i = 0; i < bullets-1; ++i ) {
			Instantiate(projectile);
			projectile.transform.Rotate( 0, angle/(bullets-1), 0 );
		}
		
	}
	
}


public enum WeaponName {
	Bullet,
	Laser,
	SplitBullet,
	SplitLaser
}