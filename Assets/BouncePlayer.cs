using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour {
	
	void OnCollisionEnter (Collision col)
	{
//		Destroy(col.gameObject);

		if(col.gameObject.name == "Floor")
		{
			print (gameObject.name);
//			Destroy(gameObject);
//			print ("HEY");
//			GameObject player = GameObject.Find (col.gameObject.name);
//			player.transform.position.Set (0, 0, 10);

		}
	}
}
