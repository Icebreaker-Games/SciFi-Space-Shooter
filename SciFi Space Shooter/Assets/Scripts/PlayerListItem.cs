using Steamworks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Samuel Larson
//Last Modified: 2/13/2025
//By Samuel Larson

namespace IBG
{
    public class PlayerListItem : MonoBehaviour
    {
        public string PlayerName;
        public int ConnectionID;
        public ulong PlayerSteamID;
        private bool AvatarReceived;

        public TextMeshProUGUI PlayerNameText;
        public RawImage PlayerIcon;

        protected Callback<AvatarImageLoaded_t> ImageLoaded;

        private void Start()
        {
            ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
        }

        private void OnImageLoaded(AvatarImageLoaded_t callback)
        {
            if(callback.m_steamID.m_SteamID == PlayerSteamID)
            {
                PlayerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
            }
            else
            {
                return;
            }
        }

        private Texture2D GetSteamImageAsTexture(int iImage)
        {
            Texture2D texture = null;

            bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
            if(isValid)
            {
                byte[] image = new byte[width * height * 4];

                isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

                if(isValid)
                {
                    texture = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false, true);
                    texture.LoadRawTextureData(image);
                    texture.Apply();
                }
            }
            AvatarReceived = true;
            return texture;
        }

        void GetPlayerIcon()
        {
            int ImageID = SteamFriends.GetLargeFriendAvatar((CSteamID)PlayerSteamID);
            if(ImageID == -1) { return; }
            PlayerIcon.texture = GetSteamImageAsTexture(ImageID);
        }

        public void SetPlayerValues()
        {
            PlayerNameText.text = PlayerName;
            if(!AvatarReceived) { GetPlayerIcon(); }
        }
    }
}