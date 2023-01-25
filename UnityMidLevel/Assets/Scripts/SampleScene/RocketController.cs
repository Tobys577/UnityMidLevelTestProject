using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace UnityMidLevel.SampleScene
{
    /// <summary>
    /// Controls the player's rocket controls and collisions
    /// </summary>
    public class RocketController : MonoBehaviour
    {
        [Tooltip("The Crash Restarter prefab found in the assets")]
        [SerializeField]
        private GameObject crashRestarterPrefab;

        [Tooltip("How long to wait before respawn after crash")]
        [SerializeField]
        private float respawnTime;

        [SerializeField]
        private float maxThrottle;
        [Tooltip("The rate at which the throttle of the rocket will increase when forward is held, and decrease when back is held")]
        [SerializeField]
        private float throttleIncrease;
        [Range(0.05f, 0.5f)]
        [SerializeField]
        private float speedSmoothing;
        [Tooltip("The default starting throttle for the rocket")]
        [SerializeField]
        private float throttle;

        [SerializeField]
        private float rotationSpeed;

        [Tooltip("The thruster sprite attached to the rocket character")]
        [SerializeField]
        private SpriteRenderer thrusterSprite;

        // The photon view component attached to this object
        private PhotonView photonView;
        private Rigidbody rb;

        private Vector3 startPosition;
        private Quaternion startRotation;

        // Used for the player controlls
        private Vector3 velocityReference;

        void Start()
        {
            photonView = GetComponent<PhotonView>();
            rb = GetComponent<Rigidbody>();

            startPosition = transform.position;
            startRotation = transform.rotation;
        }

        /// <summary>
        /// Used to synchronise enabling / disabling rocket across clients
        /// </summary>
        [PunRPC]
        public void SetVisible(bool value)
        {
            gameObject.SetActive(value);
        }

        void Update()
        {
            if (photonView.IsMine)
            {
                // Only enable the rigidbody movement once both rockets are loaded in
                if (rb.isKinematic)
                {
                    rb.isKinematic = !(PhotonNetwork.CurrentRoom.PlayerCount == 2);
                    return;
                }

                // Apply movement based on WSAD / Arrow Keys using Input.GetAxis()
                transform.Rotate(new Vector3(0, 0, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime));
                throttle += Input.GetAxis("Vertical") * throttleIncrease;
                throttle = Mathf.Clamp(throttle, 0, maxThrottle);
                Vector3 targetVelocity = transform.up * throttle;
                rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocityReference, speedSmoothing);

                // Change the alpha of the rocket's thruster based on the current throttle.
                Color thrusterSpriteColour = thrusterSprite.color;
                thrusterSpriteColour.a = throttle / maxThrottle;
                thrusterSprite.color = thrusterSpriteColour;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Check you haven't somehow collided with yourself
            if (collision.gameObject != gameObject)
            {
                // Instantiate a crash restarter object to handle the restart process for the rocket
                CrashRestarter crashRestarter = Instantiate(crashRestarterPrefab).GetComponent<CrashRestarter>();
                crashRestarter.rocketObject = gameObject;
                crashRestarter.waitTime = respawnTime;
                crashRestarter.startPosition = startPosition;
                crashRestarter.startRotation = startRotation;

                Debug.Log("Crashed!");
            }
        }
    }
}
