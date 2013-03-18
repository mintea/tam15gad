using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour 
{
	
	public float playerSpeed;
	public float hitTimer;
	public const float invincibleTime = 2;
	public Rigidbody bullet;
	public Transform deathExplosion;
	public int playerLives;
	
	// Use this for initialization
	void Start () 
	{
		hitTimer = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Amount to move player
		float amtToMoveX = playerSpeed * Input.GetAxis ("Horizontal") * Time.deltaTime;
		float amtToMoveY = playerSpeed * Input.GetAxis("Vertical") * Time.deltaTime;	
		
		//Moves the player
		transform.Translate(Vector3.right * amtToMoveX);
		transform.Translate (Vector3.forward * amtToMoveY);
		
		//Shoots bullet
		if (Input.GetButtonDown ("Fire1"))
		{
			Rigidbody tempBullet;
			
			tempBullet = Instantiate(bullet, transform.position, transform.rotation) as Rigidbody;
		}
	}
	
	//Need to figure out how to destroy the game object while keeping the player lives intact
	//Possibly create a new script called gameScript and run all processes there so we can 
	//destroy gameObjects such as player, but keep data.
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			if (Time.time > hitTimer) {
				Transform tempExplosion;
				
				//Creates an explosion when enemy touches player.
				tempExplosion = Instantiate (deathExplosion, transform.position, transform.rotation) as Transform;
				
				playerLives--;
				if (playerLives>0){
					hitTimer = Time.time + invincibleTime;
					StartCoroutine (BlinkPlayer(2));
				}
				else {
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
		renderer.enabled = true; // when done blinking make sure player is shown
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
