using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	public int maxDistance;
	public int moveSpeed;
	public int rotationSpeed;
	public Rigidbody bullet;
	public float rotationDamping = 20f;
	public float attackTimer;

	private Transform myTransform;
	
	// Use this for initialization
	void Start () {
		attackTimer = 0;
		myTransform = transform;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
		myTransform.parent = player.transform;
		//InvokeRepeating("Shoot",1,0.5f);
		maxDistance = 2;
	}
	
	void UpdateMovement()
	{
		float x = Input.GetAxis ("HorizontalFire");
		float z = Input.GetAxis ("VerticalFire");
		
		Vector3 inputVec = new Vector3(x, 0, z);
		inputVec *= moveSpeed;
		
		if (inputVec != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(inputVec), 
                Time.deltaTime * rotationDamping);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMovement();
		if (Mathf.Abs(Input.GetAxis ("HorizontalFire")) > 0.0000001f || Mathf.Abs (Input.GetAxis ("VerticalFire")) > 0.0000001f){
			if (Time.time > attackTimer) {
				Shoot ();
				attackTimer = Time.time + 0.1f;
			}
		}
	}
	
	public void Shoot() {
		myTransform.position += myTransform.forward * 1;
		Instantiate(bullet, myTransform.position, myTransform.rotation);
		myTransform.position -= myTransform.forward * 1;
	}
}
