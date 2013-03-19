using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour 
{
	
	public float playerSpeed;
	public float hitTimer;
	public float rotationDamping = 20;
	public const float invincibleTime = 2;
	public Transform deathExplosion;
	public int playerLives;
	
	// Use this for initialization
	void Start () 
	{
		hitTimer = 0;
		playerSpeed = 5;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get Joystick Direction
		float x = Input.GetAxis ("Horizontal");
		float z = Input.GetAxis ("Vertical");
		
		// Rotate the player
		Vector3 inputVec = new Vector3(x, 0, z);
		inputVec *= playerSpeed;
		
		if (inputVec != Vector3.zero)
			transform.rotation = Quaternion.Slerp(transform.rotation,
				Quaternion.LookRotation(inputVec),
				Time.deltaTime * rotationDamping);
		
		// Move the player
		x *= playerSpeed * Time.deltaTime;
		z *= playerSpeed * Time.deltaTime;
		
		transform.Translate(x,0,z,Space.World);
	}
	
	
	//Need to figure out how to destroy the game object while keeping the player lives intact
	//Possibly create a new script called gameScript and run all processes there so we can 
	//destroy gameObjects such as player, but keep data.
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			if (Time.time > hitTimer) {
				// Creates an explosion when enemy touches player.
				Instantiate (deathExplosion, transform.position, transform.rotation);
				
				playerLives--;
				// If player still has lives, blink on hit
				if (playerLives>0){
					hitTimer = Time.time + invincibleTime;
					StartCoroutine (BlinkPlayer(2));
				}
				else {
				// Player does not have anymore lives. Destroy!
					StartCoroutine (WaitAndDestroyPlayer(1));
				}
			}
		}
	}
	
	
	IEnumerator BlinkPlayer(float blinkTime) {
		float endTime = Time.time + blinkTime;
		
		while (Time.time < endTime) {
			yield return new WaitForSeconds(0.2f);
			renderer.enabled = !renderer.enabled;
		}
		
		// when done blinking make sure player is shown
		renderer.enabled = true;
	}
	
	
	IEnumerator WaitAndDestroyPlayer(float waitTime) {
		renderer.enabled = false;
		playerSpeed = 0;
		float endTime = Time.time + waitTime;
		
		while (Time.time < endTime){
			yield return 0;
		}
		
		Destroy (gameObject);
		Application.LoadLevel(1);
	}
}
