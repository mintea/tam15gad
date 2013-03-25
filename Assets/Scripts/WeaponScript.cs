using UnityEngine;
using System.Collections;

enum WEAPS{
	Default = 0,
	Laser,
	Split,
}

public class WeaponScript : MonoBehaviour {
	public float attackTimer;
	private float x;
	private float z;
	public int maxDistance;
	public int Weapon = 0;
	public Rigidbody[] bullet = new Rigidbody[5];


	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		maxDistance = 0; // not used
		attackTimer = 0;
		
		myTransform = transform;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		myTransform.parent = player.transform;
		//InvokeRepeating("Shoot",1,0.5f); // Can use for Autoshooting
		maxDistance = 2;
	}
	
	void UpdateRotation()
	{
		Vector3 inputVec = new Vector3(x, 0, z);
		
		if (inputVec != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(inputVec);
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxis ("HorizontalFire");
		z = Input.GetAxis ("VerticalFire");
		UpdateRotation();

		if (Mathf.Abs(x) > 0.0000001f || Mathf.Abs (z) > 0.0000001f){
			if (Time.time > attackTimer) {
				Shoot ();
				attackTimer = Time.time + 0.1f;
			}
		}
	}
	
	public void Shoot() {
		myTransform.position += myTransform.forward * 1;
		Instantiate(bullet[Weapon], myTransform.position, myTransform.rotation);
		myTransform.position -= myTransform.forward * 1;
	}
}
