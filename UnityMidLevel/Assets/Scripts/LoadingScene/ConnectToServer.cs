using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace UnityMidLevel.LoadingScene
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        [Tooltip("The name of the main game scene found in the build settings")]
        [SerializeField]
        private string gameSceneName;

        /// <summary>
        /// Connect to the server using the defualt settings preset in the Unity Editor
        /// </summary>
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        /// <summary>
        /// When the server is connected, load into a lobby to begin matchmaking
        /// </summary>
        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            PhotonNetwork.JoinLobby();
        }

        /// <summary>
        /// When a lobby is joined, start matchmaking procedure
        /// </summary>
        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();

            // Join any open rooms or create a new room. Limit the players to 2, one for each rocket.
            RoomOptions roomOptionsX = new RoomOptions();
            roomOptionsX.MaxPlayers = 2;
            PhotonNetwork.JoinRandomOrCreateRoom(roomOptions: roomOptionsX);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            PhotonNetwork.LoadLevel(gameSceneName);
        }
    }
}
