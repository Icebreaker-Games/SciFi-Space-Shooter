using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

//Samuel Larson
//Last Modified 2/13/2024
//By Samuel Larson

namespace IBG
{
    public class LobbyDataEntry : MonoBehaviour
    {
        public CSteamID lobbyID;
        public string lobbyName;
        public TextMeshProUGUI lobbyNameText;

        public void SetLobbyData()
        {
            if(lobbyName == "")
            {
                lobbyNameText.text = "Empty";
            }
            else
            {
                lobbyNameText.text = lobbyName;
            }
        }

        public void JoinLobby()
        {

        }
    }
}