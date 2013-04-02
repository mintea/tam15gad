using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour 
{
	
	//Enemy Prefabs
	public GameObject[] enemies;
	
	public bool spawn = true;
	public float spawnTimer = 0.0f;
	public float spawnCooldown = 0.0f;
	
	private Transform _transform;
	
	// Use this for initialization
	void Start () 
	{
		_transform = transform;
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
//		Debug.Log (spawnTimer);
	}
	
	private void spawnEnemy()
	{
//		GameObject Enemy = (GameObject) Instantiate(SuicideEnemy, _transform.position+_transform.forward, _transform.rotation);
		Instantiate(enemies[Random.Range(0,Enemies.Length)], _transform.position+_transform.forward, _transform.rotation);
	}

}
