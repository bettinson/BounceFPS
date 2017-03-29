using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
		
	private bool m_Jump;
	private bool m_Jumping;
	private float distToGround;
	private Rigidbody body;
	private CharacterController m_CharacterController;
	private Vector3 m_MoveDir = Vector3.zero;
	[SerializeField] private float m_JumpSpeed;


	// Use this for initialization
	void Start () {
//		body = gameObject.GetComponent<Rigidbody>;
		m_MoveDir.y = -5;
		distToGround = 10;
//		m_CharacterController = gameObject.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

			if (m_Jump)
			{
				m_MoveDir.y = m_JumpSpeed;
//				PlayJumpSound();
				m_Jump = false;
				m_Jumping = true;
			}
	}

//	bool IsGrounded() {
//		return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1);
//	}
//
	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
//		Rigidbody body = GetComponent<Rigidbody>;
		GameObject hitGameObject = hit.collider.gameObject;

		// Bounce on contact with the floor
		if (hitGameObject != null) {
			if (hitGameObject.tag == "Bouncy") {
				m_Jump = true;
				print ("AI has hit ground");
//				body.AddForceAtPosition(m_CharacterController.velocity*0.5f, hit.point, ForceMode.Impulse);
			}

			if (hitGameObject.name == "Death") {
				death ();
				print ("Player died");
				// TODO Get last person who hit them and incremement their counter
			}
		}


		//dont move the rigidbody if the character is on top of it
//		if (m_CollisionFlags == CollisionFlags.Below)
//		{
//			return;
//		}

//		if (body == null || body.isKinematic)
//		{
//			return;
//		}

//		body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
	}
	private void death() {
		Vector3 respawnPoint = new Vector3(0, 50, 0);
		gameObject.transform.localPosition = respawnPoint;
	}
}
