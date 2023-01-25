using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace UnityMidLevel.SampleScene
{
    public class SpawnRockets : MonoBehaviour
    {
        [Tooltip("The prefab for the rocket found in the Resources folder")]
        [SerializeField]
        private GameObject rocketPrefab;

        [Tooltip("The rocket spawn positions found within the map")]
        [SerializeField]
        private Transform[] rocketSpawnPositions;

        /// <summary>
        /// Initated the rocket object when the player loads in.
        /// Place it at the correct position depending on number of players in hte lobby.
        /// </summary>
        private void Start()
        {
            PhotonNetwork.Instantiate(rocketPrefab.name, rocketSpawnPositions[PhotonNetwork.CurrentRoom.PlayerCount - 1].position, rocketPrefab.transform.rotation)
            .GetComponent<RocketController>();
        }
    }
}