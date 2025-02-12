using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//Samuel Larson
//Last Modified: 2/12/2024
//By Samuel Larson

namespace IBG
{
    public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
    {
        public TMP_InputField createInput;
        public TMP_InputField joinInput;

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(createInput.text);
        }

        public void JoinRoom()
        {
            PhotonNetwork.JoinRoom(joinInput.text);
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.LoadLevel("Networking Test");
        }
    }
}