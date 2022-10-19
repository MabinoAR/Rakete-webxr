using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IronHead.MVRTFusion
{
    public class LoginUI : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;

        public void SetPlayerName()
        {
            LocalPlayerData.PlayerName = inputField.text;
        }
        public void LoadHomeScene()
        {
            NetworkManager.Instance.GetComponent<NetworkHelper>().ChangeMap(Fusion.GameMode.Single, "HomeScene", "HomeScene", 0);
        }
    }
}