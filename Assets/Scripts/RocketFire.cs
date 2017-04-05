using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFire : MonoBehaviour {
	float rocketSpeed = 25;
	float bulletSpeed = 70;
	public GameObject rocketPrefab;
	public Transform rocketSpawn;
	private Vector3 rotation = new Vector3(90,0,0);

	// Use this for initialization
	void Start () {
		rocketPrefab.gameObject.transform.Rotate (rotation);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			AudioSource gunSound = GetComponent<AudioSource>();

			gunSound.Play ();
			FireRocket();
			GetComponent<Animation>().Play ("RocketShot");


			// Add force to the cloned object in the object's forward direction
//			clone.rigidBody.AddForceAt(clone.transform.forward * 1000);
		}

		if (Input.GetButtonDown ("Fire2")) {
			AudioSource gunSound = GetComponent<AudioSource>();
			gunSound.Play ();
			FireBullet();
			GetComponent<Animation>().Play ("RocketShot");
		}
	}

	void FireRocket()
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

	void FireBullet()
	{
		// Create the Bullet from the Bullet Prefab
		var rocket = (GameObject)Instantiate(
			rocketPrefab,
			rocketSpawn.position,
			rocketSpawn.rotation);

		// Add velocity to the bullet
		rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * bulletSpeed;

		// Destroy the bullet after 2 seconds
		Destroy(rocket, 30.0f); 
	}
}
