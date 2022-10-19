using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;


namespace IronHead.MVRTFusion
{
    public class SessionItem : MonoBehaviour
    {
        [SerializeField] private Text _name;
        [SerializeField] private Text _map;
        [SerializeField] private Text _players;


        private Action<SessionInfo> _onJoin;
        private SessionInfo _info;
        public void UpdateItem(SessionInfo info, Action<SessionInfo> onJoin)
        {
            _info = info;
            _name.text = info.Name;
            info.Properties.TryGetValue("mapName", out SessionProperty sessionProperty);
            _players.text = "Player:" + info.PlayerCount + " / " + info.MaxPlayers;

            _map.text = (string)sessionProperty.PropertyValue;
            _onJoin = onJoin;

        }
        public void OnJoin()
        {
            _onJoin(_info);
        }


    }
}