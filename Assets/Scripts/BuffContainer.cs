using UnityEngine;
using System.Collections;
using System;

public class BuffContainer : MonoBehaviour {
	
	public float destroyTime = 10f;
	public WeaponName weapon;
	public Color[] boxColor = {
		new Color(0,0.5f,1),
		Color.red,
		Color.yellow,
		new Color(1,0.3f,0)
	};
	
	// Use this for initialization
	void Start () {
		weapon = (WeaponName)(UnityEngine.Random.Range(1,Enum.GetValues(typeof(WeaponName)).Length));
		renderer.material.color = boxColor[((int)weapon)-1];
		Destroy(gameObject, destroyTime);
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			collider.GetComponent<Player>().SetBuff((int)weapon);
			Destroy(gameObject);
		}
	}

}
