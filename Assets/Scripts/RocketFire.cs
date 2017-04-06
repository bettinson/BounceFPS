using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFire : MonoBehaviour {
	public GameObject rocketPrefab;
	public Transform rocketSpawn;
	private float timer;
	private AudioSource gunSound;

	// Use this for initialization
	void Start () {
		gunSound = GetComponent<AudioSource>();
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
//		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
//		var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
//
//		transform.Rotate(0, x, 0);
//		transform.Translate(0, 0, z);

		if (Input.GetButtonDown ("Fire1") && timer >= 2.0) {
			timer = 0;
			gunSound.Play ();
			Fire();
			GetComponent<Animation>().Play ("RocketShot");


			// Add force to the cloned object in the object's forward direction
//			clone.rigidBody.AddForceAt(clone.transform.forward * 1000);
		}
		/*if (Input.GetButtonDown("Fire2")) {
			gunSound.Play();
			GetComponent<Animation>().Play ("RocketShot");
		}*/

		timer += Time.deltaTime;
	}

	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var rocket = (GameObject)Instantiate(
			rocketPrefab,
			rocketSpawn.position,
			rocketSpawn.rotation);
		rocket.AddComponent<RocketForce>();

		// Add velocity to the bullet
		rocket.GetComponent<Rigidbody>().velocity = rocket.transform.forward * 50;
		Destroy(rocket, 2f);
	}
}
