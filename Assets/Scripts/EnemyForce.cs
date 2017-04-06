using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyForce : MonoBehaviour {

	private CharacterController controller;
	private float push;
	private float timer;
	private Vector3 force;

	// Use this for initialization
	void Start () {
		push = 35;
		timer = 3;
		force = new Vector3(1,1,1);
	}

	void Update() {
		if (timer < 2) {
			controller.Move(force);
		}
		else {
			force *= 0;
		}
		timer += Time.deltaTime;
	}
	
	void OnCollisionEnter (Collision col) {
		GameObject obj = col.gameObject;
		string tag = col.gameObject.tag;

		if (tag == "Player" || tag == "Ally") {
			Vector3 dirvec = col.relativeVelocity.normalized;

			controller = obj.GetComponent<CharacterController> ();
			//if (push == 35) {
			//	dirvec *= Math.Max(dirvec.x, Math.Max(dirvec.y, dirvec.z));
			//}
			force = dirvec * push * Time.deltaTime * -1;
			timer = 0;
			Destroy (GetComponent<Collider> ());
			Destroy (GetComponent<Rigidbody> ());
		} else if (tag != "Ally" && tag != "Player" && tag != "Enemy") {
			transform.localScale += new Vector3 (2f, 2f, 2f);
			Destroy (GetComponent<Rigidbody> (), 0.5f);
			Destroy (GetComponent<Collider> (), 0.5f);
			Destroy (GetComponent<Renderer> (), 0.5f);
		} else {
			print ("WE ARE HITTING: " + tag);
		}
		
	}
}
