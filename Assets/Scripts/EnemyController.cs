using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.
		[SerializeField] private AudioClip m_deathSound; 

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        private float speed; //added
        private Vector3 desiredMove; //added
        private float timer = 0; //added

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            //float speed;
            //GetInput(out speed); speed changed to speed
            // always move along the camera forward as it is the direction that it being aimed at
            //Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x; //removed and moved to GetInput

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;
            

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
  //          UpdateCameraPosition(speed);

           // m_MouseLook.UpdateCursorLock();

            m_CharacterController.transform.LookAt(m_Camera.transform.position);
            m_CharacterController.transform.Rotate(0, 90, 0, Space.Self);

            if ( timer >= 2.0 && m_CharacterController.transform.position.x > -25 && m_CharacterController.transform.position.x < 25 && m_CharacterController.transform.position.z > -25 && m_CharacterController.transform.position.z < 25) { //added
                m_CharacterController.Move(m_CharacterController.transform.right * 500 * Time.deltaTime); //added
                timer = 0; //added
            } //added
            timer += Time.deltaTime; //added*/
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
        }


        private void UpdateCameraPosition(float speed)
        {
        }

//		void OnCollisionEnter (Collision col)
//		{
//			//		Destroy(col.gameObject);
//			print (gameObject.name);
//
//			if(col.gameObject.name == "Floor")
//			{
//				//			Destroy(gameObject);
//				//			print ("HEY");
//				//			GameObject player = GameObject.Find (col.gameObject.name);
//				//			player.transform.position.Set (0, 0, 10);
//
//			}
//		}
//
        private float GetInput() //now returns a float. no longer takes out speed
        {
            // Read input
            float horizontal = Random.Range(-1,2);
            float vertical = Random.Range(-1,2);
            if (m_CharacterController.transform.position.x > 25) {
                horizontal = Random.Range(-1,1);
            }
            else if (m_CharacterController.transform.position.x < -25) {
                horizontal = Random.Range(0,2);
            }
            if (m_CharacterController.transform.position.z > 25) {
                vertical = Random.Range(-1,1);
            }
            else if (m_CharacterController.transform.position.z < -25) {
                vertical = Random.Range(0,2);
            }

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
            return speed;
        }


        private void RotateView()
        {
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
			GameObject gameObject = hit.collider.gameObject;

			// Bounce on contact with the floor
			if (gameObject != null) {
				if (gameObject.tag == "Bouncy") {
					m_Jump = true;
//					body.AddForceAtPosition(m_CharacterController.velocity*0.5f, hit.point, ForceMode.Impulse);
                    speed = GetInput(); //added
                    desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;
				}

				if (gameObject.name == "Death") {
					death (m_CharacterController);
//					print ("Player died");
					// TODO Get last person who hit them and incremement their counter
				}
			}


            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }

            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }

		// Respawn player
		private void death(CharacterController deadPlayer) {
			m_AudioSource.clip = m_deathSound;
			m_AudioSource.Stop(); 
			m_AudioSource.Play();

			Vector3 respawnPoint = new Vector3(0, 50, 0);
			deadPlayer.gameObject.transform.localPosition = respawnPoint;
        }
    }
}
