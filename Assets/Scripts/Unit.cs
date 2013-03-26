using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public GameObject deathAnimation;
	public Vector3 direction = Vector3.zero;	// the direction of movement
	
	public float moveSpeed;
	public float rotSpeed;
	public float invincibleTime;				// the time that the unit is invincible after being damaged
	
	public int   curHealth;
	public int   maxHealth;
	
	public int   curWeapon;
	
	public bool  isInvincible;					// flag to check if unit is invincible (ex. after getting damaged)
	
	protected Transform _transform;
	
	
	
	public void AdjustHealth(int points) {
		
		if (points > 0) {			// getting healed
			curHealth += points;
			if (curHealth > maxHealth)
				curHealth = maxHealth;
		}
		
		else if (points < 0) {		// getting damaged
			if (!isInvincible) {
				curHealth += points;
				if (curHealth > 0)
					StartCoroutine(InvincibleForTime (invincibleTime));
				else
					KillUnit();
			}
		}
	}
	
	protected void SetDirection(float x, float z) {
		direction.x = x;
		direction.z = z;
	}
	
	
	protected void MoveUnit() {
		_transform.position += _transform.forward * moveSpeed * Time.deltaTime;
	}
	
	
	protected void RotateUnit() {
		_transform.rotation =
//				Quaternion.LookRotation(direction);
				Quaternion.Lerp (
						_transform.rotation, 
						Quaternion.LookRotation(direction),
						rotSpeed * Time.deltaTime
				);
	}
	
	
	IEnumerator InvincibleForTime(float waitTime) {
		isInvincible = true;
		StartCoroutine(BlinkUnit(waitTime, 5));
		yield return new WaitForSeconds(waitTime);
		isInvincible = false;
	}
	
	
	IEnumerator BlinkUnit(float blinkTime, int blinks) {
		float endTime = Time.time + blinkTime;
		
		while (Time.time < endTime) {
			yield return new WaitForSeconds(blinkTime/2/blinks);
			renderer.enabled = !renderer.enabled;	// blink unit
		}
		
		// when done blinking make sure unit is shown
		renderer.enabled = true;
	}
	
	public void KillUnit() {
		
		renderer.enabled = false;		// hide unit
		moveSpeed = 0;					// make sure unit can't move
		
		// trigger death animaiton
		Instantiate (deathAnimation, _transform.position, Quaternion.identity);
		
		Destroy (gameObject);			// destroy this unit
	}
	
}
