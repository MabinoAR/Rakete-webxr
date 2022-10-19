using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace IronHead.MVRTFusion
{
    public class UIManager : MonoBehaviour
    {
        public enum UI_Page { Main, SessionList }

        [SerializeField] TMP_InputField InputFieldSessionName;

        private string Map = "NetworkedScene";

        public void SetGameMap(string map)
        {
            Map = map;
        }
        public void HostGame()
        {


            NetworkManager.Instance.GetComponent<NetworkHelper>().ChangeMap(Fusion.GameMode.Host, InputFieldSessionName.text, this.Map, 1);
        }


    }
}