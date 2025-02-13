using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using IBG;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI lobbyNameText;

    [Header("Player Data")]
    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    [Header("Other Data")]
    public ulong currentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;

    [Header("Manager")]
    private CustomNetworkManager manager;

    private CustomNetworkManager Manager
    {
        get
        {
            if (manager != null)
            {
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void UpdateLobbyName()
    {
        currentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        lobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(currentLobbyID), "name");
    }

    public void UpdatePlayerList()
    {
        if(!PlayerItemCreated){ CreateHostPlayerItem(); } //Host
        if(PlayerListItems.Count < Manager.GamePlayers.Count) { CreateClientPlayerItem(); }
        if (PlayerListItems.Count > Manager.GamePlayers.Count) { RemovePlayerItem(); }
        if(PlayerListItems.Count == Manager.GamePlayers.Count) { UpdatePlayerItem(); }
    }

    public void FindLocalPlayer()
    {
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem()
    {
        foreach(PlayerObjectController player in Manager.GamePlayers)
        {
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.SetPlayerValues();

            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }

    public void CreateClientPlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            if(!PlayerListItems.Any(b => b.ConnectionID == player.ConnectionID))
            {
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.SetPlayerValues();

                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem()
    {
        foreach (PlayerObjectController player in Manager.GamePlayers)
        {
            foreach(PlayerListItem PlayerListItemScript in PlayerListItems)
            {
                if(PlayerListItemScript.ConnectionID == player.ConnectionID)
                {
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.SetPlayerValues();
                }
            }
        }
    }

    public void RemovePlayerItem()
    {
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach(PlayerListItem playerListItem in PlayerListItems)
        {
            if(!Manager.GamePlayers.Any(b => b.ConnectionID == playerListItem.ConnectionID))
            {
                playerListItemToRemove.Add(playerListItem);
            }
        }
        if(playerListItemToRemove.Count > 0)
        {
            foreach( PlayerListItem playerListItem in playerListItemToRemove)
            {
                GameObject ObjectToRemove = playerListItem.gameObject;
                PlayerListItems.Remove(playerListItem);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }
}
