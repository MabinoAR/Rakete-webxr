using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class PlayerObject : NetworkBehaviour
    {
        [SerializeField] PlayerData _playerData;

        [Networked(OnChanged = nameof(PlayerNameChange))]
        public string PlayerName { get; set; }

        [Networked(OnChanged = nameof(ColorChange))]
        public int PlayerColor { get; set; }


        [Networked(OnChanged = nameof(BodyChange))]
        public int PlayerBody { get; set; }


        [Networked(OnChanged = nameof(HeadChange))]
        public int PlayerHead { get; set; }




        private void Start()
        {


            if (Object.HasInputAuthority)
            {
                _playerData.SetNetworkPlayer();
            }
        }

        public void SetClient()
        {
            _playerData.SetUpClientPlayer();

        }
        public void SetUpPlayer()
        {

            Rpc_SetPlayerName(LocalPlayerData.PlayerName);
            Rpc_SetColour(LocalPlayerData.PlayerColor);
            Rpc_SetBody(LocalPlayerData.PlayerBody);
            Rpc_SetHead(LocalPlayerData.PlayerHead);
        }


        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void Rpc_SetPlayerName(string playerName)
        {
            PlayerName = playerName;
        }
        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void Rpc_SetColour(int color)
        {
            PlayerColor = color;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void Rpc_SetBody(int index)
        {
            PlayerBody = index;
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
        public void Rpc_SetHead(int index)
        {
            PlayerHead = index;
        }
        static void PlayerNameChange(Changed<PlayerObject> changed)
        {

            changed.Behaviour._playerData.SetPlayerName(changed.Behaviour.PlayerName);
        }
        static void ColorChange(Changed<PlayerObject> changed)
        {
            changed.Behaviour._playerData.SetColour(changed.Behaviour.PlayerColor);
        }
        static void BodyChange(Changed<PlayerObject> changed)
        {
            changed.Behaviour._playerData.SetBody(changed.Behaviour.PlayerBody);
        }
        static void HeadChange(Changed<PlayerObject> changed)
        {
            changed.Behaviour._playerData.SetHead(changed.Behaviour.PlayerHead);
        }

    }
}