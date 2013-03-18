using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour 
{
	
	public float playerSpeed;
	public Rigidbody bullet;
	public Transform deathExplosion;
	public int playerLives;
	
	// Use this for initialization
	void Start () 
	{
		
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
		
		if (playerLives == 0)
		{
			Destroy (gameObject);
			Application.LoadLevel(1);
		}
	}
	
	//Need to figure out how to destroy the game object while keeping the player lives intact
	//Possibly create a new script called gameScript and run all processes there so we can 
	//destroy gameObjects such as player, but keep data.
	void OnTriggerEnter(Collider enemy)
	{
		if (enemy.gameObject.tag == "enemy")
		{
			Transform tempExplosion;
			
			//Creates an explosion when enemy touches player.
			tempExplosion = Instantiate (deathExplosion, transform.position, transform.rotation) as Transform;
			
			playerLives--;
		}
	}
}
