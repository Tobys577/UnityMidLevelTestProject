using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMidLevel.SampleScene
{
    [RequireComponent(typeof(PhotonView))]
    public class SetGravitySingleton : MonoBehaviourPun
    {
        public static SetGravitySingleton instance;

        // Ensure that only one instance is in the scene
        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(gameObject);
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            instance.CallSetGravity(PlayerPrefs.GetFloat("Gravity"));
        }

        // Used to synchronize the latest gravity to all clients in a room
        public void CallSetGravity(float gravity)
        {
            photonView.RPC("SetGravity", RpcTarget.All, gravity);
        }

        // When someone runs the CallSetGravity method, this function will run on all clients
        [PunRPC]
        public void SetGravity(float gravityValue)
        {
            Physics2D.gravity = new Vector2(0, -gravityValue);
            Debug.Log($"Gravity set: { gravityValue }");
        }
    }
}
