using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceOnFloorCollision : MonoBehaviour {
	
	void OnCollisionEnter (Collision col)
	{
//		print ("HEY");
//		Destroy(col.gameObject);

		if(col.gameObject.name == "Capsule")
		{
			print ("Found capsule");
//			print ("HEY");
//			GameObject player = GameObject.Find (col.gameObject.name);
//			player.transform.position.Set (0, 0, 10);

		}
	}
}
