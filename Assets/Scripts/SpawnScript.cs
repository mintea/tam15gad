using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour 
{
	
	//Enemy Prefabs
	public GameObject SuicideEnemy;
	
	public bool spawn = true;
	public float spawnTimer = 0.0f;
	public float spawnCooldown = 0.0f;
	
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		if (spawnTimer >= spawnCooldown)
		{
			spawnEnemy();
			
			spawnTimer = 0;
		}
		else 
		{
			spawnTimer += Time.deltaTime;
		}
		Debug.Log (spawnTimer);
	}
	
	private void spawnEnemy()
	{
		GameObject Enemy = (GameObject) Instantiate(SuicideEnemy, gameObject.transform.position, Quaternion.identity);
	}

}
