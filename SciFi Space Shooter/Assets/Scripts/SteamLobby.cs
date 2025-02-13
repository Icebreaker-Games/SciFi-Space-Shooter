using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using TMPro;
using Unity.VisualScripting;

//Samuel Larson
//Last Modified: 2/14/2025
//By Samuel Larson

namespace IBG
{
    public class SteamLobby : MonoBehaviour
    {
        //Callbacks
        protected Callback<LobbyCreated_t> LobbyCreated;
        protected Callback<GameLobbyJoinRequested_t> JoinRequest;
        protected Callback<LobbyEnter_t> LobbyEntered;

        //Variables
        public ulong CurrentLobbyID;
        private const string HostAddressKey = "HostAddress";
        private CustomNetworkManager manager;

        [Header("GameObjects")]
        public GameObject BrowserPanel;
        public GameObject LobbyPanel;

        [Header("Buttons")]
        public GameObject HostButton;
        public TextMeshProUGUI LobbyNameText;
        public TextMeshProUGUI PublicPrivateText;

        public static SteamLobby Instance;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
                GameObject.Destroy(this.gameObject);
        }

        private void Start()
        {
            if(!SteamManager.Initialized) { return; }
            manager = GetComponent<CustomNetworkManager>();

            LobbyCreated = Callback<LobbyCreated_t>.Create(OnlobbyCreated);
            JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(OnJoinRequest);
            LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

        }

        public void HostLobbyPublic()
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, manager.maxConnections);
        }

        public void HostLobbyFriends()
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
        }

        private void OnlobbyCreated(LobbyCreated_t callback)
        {
            if (callback.m_eResult != EResult.k_EResultOK) { return; }

            Debug.Log("Lobby created Successfully");

            manager.StartHost();

            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey, SteamUser.GetSteamID().ToString());
            SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString() + "'s Lobby");
        }

        private void OnJoinRequest(GameLobbyJoinRequested_t callback)
        {
            Debug.Log("Request To Join Lobby");
            SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        }

        private void OnLobbyEntered(LobbyEnter_t callback)
        {
            //Everyone
            BrowserPanel.SetActive(false);
            LobbyPanel.SetActive(true);
            CurrentLobbyID = callback.m_ulSteamIDLobby;
            LobbyNameText.gameObject.SetActive(true);
            LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name");

            //Clients
            if (NetworkServer.active) { return; }
            SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

            manager.StartClient();
        }
    }
}