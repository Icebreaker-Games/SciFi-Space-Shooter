using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Samuel Larson
//Last Modified: 2/12/2024
//By Samuel Larson

namespace IBG
{
    public class PlayerListManager : MonoBehaviour
    {
        [Header("UI Elements")]
        public TextMeshProUGUI list1Text;
        public TextMeshProUGUI list2Text;

        [Header("Player Lists")]
        private List<string> playerList1 = new List<string>();
        private List<string> playerList2 = new List<string>();

        public static PlayerListManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                GameObject.Destroy(this.gameObject);
        }

        void Start()
        {
            // Populate and display the lists on start
            PopulatePlayerLists();
            UpdatePlayerLists();
        }

        // Method to populate player lists (example)
        void PopulatePlayerLists()
        {
            playerList1.Clear();
            playerList2.Clear();

            for (int i = 0; i < 10; i++)
            {
                playerList1.Add("Player " + (i + 1));
                playerList2.Add("Player " + (i + 11));
            }
        }

        // Accessor method to update the player lists and refresh the UI
        public void UpdatePlayerLists()
        {
            list1Text.text = string.Join("\n", playerList1);
            list2Text.text = string.Join("\n", playerList2);
        }

        // Method to add a player to a list and update the UI
        public bool AddPlayerToList(int listNumber, string playerName)
        {
            if (listNumber == 1 && playerList1.Count < 10)
            {
                playerList1.Add(playerName);
                UpdatePlayerLists();
                return true;
            }
            else if (listNumber == 2 && playerList2.Count < 10)
            {
                playerList2.Add(playerName);
                UpdatePlayerLists();
                return true;
            }
            return false; // List is full
        }

        // Method to remove a player from a list and update the UI
        public bool RemovePlayerFromList(int listNumber, string playerName)
        {
            if (listNumber == 1 && playerList1.Remove(playerName))
            {
                UpdatePlayerLists();
                return true;
            }
            else if (listNumber == 2 && playerList2.Remove(playerName))
            {
                UpdatePlayerLists();
                return true;
            }
            return false; // Player not found
        }
    }
}