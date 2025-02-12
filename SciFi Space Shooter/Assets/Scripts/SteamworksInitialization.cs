using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Samuel Larson
//Last Modified: 2/12/2024
//By Samuel Larson

namespace IBG
{
    public class SteamworksInitialization : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Invoke("NextScene", 1f);
        }

        public void NextScene()
        {
            SceneManager.LoadScene("Photon Connection");
        }
    }
}