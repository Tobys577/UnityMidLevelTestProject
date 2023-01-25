using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityMidLevel.SampleScene
{
    /// <summary>
    /// Is instantiated on a rocket crashing to reset the rocket after a given period of time
    /// </summary>
    public class CrashRestarter : MonoBehaviour
    {
        [HideInInspector]
        public GameObject rocketObject;

        [HideInInspector]
        public float waitTime;

        [HideInInspector]
        public Vector2 startPosition;

        [HideInInspector]
        public Quaternion startRotation;

        /// <summary>
        /// Deactivate the rocket object as it has crashed.
        /// </summary>
        void Start()
        {
            rocketObject.GetPhotonView().RPC("SetVisible", RpcTarget.All, false);

            StartCoroutine(waitAndRestart());
        }

        /// <summary>
        /// Wait for specified time and reset the object to active and reset all values.
        /// </summary>
        IEnumerator waitAndRestart()
        {
            yield return new WaitForSeconds(waitTime);
            rocketObject.transform.position = startPosition;
            rocketObject.transform.rotation = startRotation;
            rocketObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            rocketObject.GetPhotonView().RPC("SetVisible", RpcTarget.All, true);
            Destroy(gameObject);
        }
    }
}
