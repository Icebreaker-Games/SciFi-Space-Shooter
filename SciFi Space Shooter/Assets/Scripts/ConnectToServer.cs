using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

//Samuel Larson
//Last Modified: 2/12/2024
//By Samuel Larson

namespace IBG
{
    public class ConnectToServer : MonoBehaviourPunCallbacks
    {
        public string playerNamePrefKey = "PlayerName";

        // Start is called before the first frame update
        void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
            SetSteamInfo();
        }

        public override void OnJoinedLobby()
        {
            SceneManager.LoadScene("Room Connection");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetSteamInfo()
        {
            string username = "";
            if (string.IsNullOrEmpty(username))
            {
                Debug.LogError("Player Name is null or empty");
                return;
            }
            PhotonNetwork.NickName = username;

            PlayerPrefs.SetString(playerNamePrefKey, username);
        }
    }
}