using UnityEngine;
using System.Collections;

public class BasketBossManager : MonoBehaviour {
	
	public GameObject[] gos = new GameObject[5];
	public BasketBoss[] ballers = new BasketBoss[5];
	public int ballHealth;
	public bool isDead=false;
	
	float _nextTime;
	
	// Use this for initialization
	void Start () {
		gos = GameObject.FindGameObjectsWithTag("BasketBoss");
		for (int i = 0; i < 5; ++i) {
			ballers[i] = gos[i].GetComponent<BasketBoss>();
			ballers[i].myIndex = i;
		}
	}
	
	void Update() {
		if (Time.time > _nextTime) {
			RotateAllPos(Random.Range(0,2));
			_nextTime = Time.time + Random.Range(1f,3f);
		}
	}
	
	void RotateAllPos(int i) {
		for (int j = 0; j < 5; ++j) {
			ballers[j].RotatePosition(i);
		}
	}
	
	public void AdjustBallHealth(int points) {
		ballHealth += points;
		if (ballHealth < 0) {
			for (int i = 4; i >= 0; --i) {
				isDead = true;
				ballers[i].KillUnit();
			}
		}
	}
}
