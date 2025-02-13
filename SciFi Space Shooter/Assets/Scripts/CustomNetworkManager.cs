using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using Steamworks;

//Samuel Larson
//Last Modified: 2/13/2025
//By Samuel Larson

namespace IBG
{
    public class CustomNetworkManager : NetworkManager
    {
        [SerializeField] private PlayerObjectController GamePlayerPrefab;
        public List<PlayerObjectController> GamePlayers { get; } = new List<PlayerObjectController>();

        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            if(SceneManager.GetActiveScene().name == "Steam Matchmaking")
            {
                PlayerObjectController GamePlayerInstance = Instantiate(GamePlayerPrefab);

                GamePlayerInstance.ConnectionID = conn.connectionId;
                GamePlayerInstance.PlayerIDNumber = GamePlayers.Count + 1;
                GamePlayerInstance.PlayerSteamID = (ulong)SteamMatchmaking.GetLobbyMemberByIndex((CSteamID)SteamLobby.Instance.CurrentLobbyID, GamePlayers.Count);

                NetworkServer.AddPlayerForConnection(conn, GamePlayerInstance.gameObject);
            }
        }
    }
}