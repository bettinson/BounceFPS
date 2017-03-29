using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	bool forward;

	// Use this for initialization
	void Start () {
		forward = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (forward) {
			float MoveForward = Input.GetAxis("Vertical") * 10 * Time.deltaTime;
			float MoveRotate = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;

			transform.Translate(Vector3.forward * MoveForward);
			transform.Rotate(Vector3.up * MoveRotate);
		}
	}
}
