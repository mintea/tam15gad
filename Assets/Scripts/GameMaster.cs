using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	
	public GameObject playerPrefab;
	public GameObject player;
	private Camera cam;
	
	public Vector3[] spawnPoints;
	
	public BoxCollider[] walls;
	
	public float viewHeight;
	public float viewWidth;
	
	public int lives;
	

	// Use this for initialization
	void Start () {
		
		SetupCamera();
		SetupWalls();
		SetupSpawnPoints(5,5);

		// Spawn Player
		SpawnPlayer(spawnPoints[Random.Range (0,spawnPoints.Length)], Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (player == null) {
			if (lives-- > 0) {
				SpawnPlayer(spawnPoints[Random.Range (0,spawnPoints.Length)], Quaternion.identity);
				return;
			} else {
				Application.LoadLevel("Lose"); // load losing screen
			}
		}
	}
	
	
	/// <summary>
	/// Spawns the player.
	/// </summary>
	/// <param name='position'>
	/// Spawn position.
	/// </param>
	/// <param name='rotation'>
	/// Spawn rotation.
	/// </param>
	void SpawnPlayer(Vector3 position, Quaternion rotation) {
		player = Instantiate(playerPrefab, position, rotation) as GameObject;
		player.name = "Player";
	}
	
	/// <summary>
	/// Setups the walls.
	/// </summary>
	void SetupWalls() {
		walls = cam.GetComponentsInChildren<BoxCollider>();
	
		walls[0].size   = Vector3.right * viewWidth;
		walls[1].size   = Vector3.up * viewHeight;
		
		walls[1].center = walls[0].size/2f;
		walls[0].center = walls[1].size/2f;
		
		walls[2].size = walls[0].size;
		walls[3].size = walls[1].size;
		
		walls[2].center = walls[0].center * -1;
		walls[3].center = walls[1].center * -1;
	}
	
	/// <summary>
	/// Setups the spawn points.
	/// Generates spawn points of size x by y
	/// </summary>
	/// <param name='x'>
	/// X.
	/// </param>
	/// <param name='y'>
	/// Y.
	/// </param>
	void SetupSpawnPoints(int x, int y) {
		spawnPoints = new Vector3[x*y];
		
		Vector3 vec = Vector3.zero;
		float w = viewWidth*0.8f;
		float h = viewHeight*0.8f;
		
		for (int i = 0; i < x; i++) {
			vec.x = i*w/(x-1) - w/2;
			for (int j=0; j < y; j++) {
				vec.z = j*h/(y-1) - h/2;
				spawnPoints[y*i+j] = vec;
			}
		}
	}
	
	/// <summary>
	/// Setups the camera.
	/// </summary>
	void SetupCamera() {
		cam = gameObject.camera;	// caches camera
		
		viewHeight = 2*cam.orthographicSize;	// caches the view's height
		viewWidth = viewHeight*cam.aspect;		// caches the view's width;
	}
		
}
