using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFire : MonoBehaviour {
	float rocketSpeed = 15;
	public GameObject rocketPrefab;
	public Transform rocketSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
//		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
//
//		transform.Rotate(0, x, 0);
//		transform.Translate(0, 0, z);

		if (Input.GetButtonDown ("Fire1")) {
			AudioSource gunSound = GetComponent<AudioSource>();
			gunSound.Play ();
			Fire();
			GetComponent<Animation>().Play ("RocketShot");


			// Add force to the cloned object in the object's forward direction
//			clone.rigidBody.AddForceAt(clone.transform.forward * 1000);
		}
	}

	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var rocket = (GameObject)Instantiate(
			rocketPrefab,
			rocketSpawn.position,
			rocketSpawn.rotation);

		// Add velocity to the bullet
		rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * rocketSpeed;

		// Destroy the bullet after 2 seconds
		Destroy(rocket, 30.0f);        
	}
}
