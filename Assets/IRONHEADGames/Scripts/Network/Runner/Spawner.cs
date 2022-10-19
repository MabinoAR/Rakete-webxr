using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class Spawner : Fusion.Behaviour
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
        Transform _spawnPoint;
        private void Awake()
        {
            var events = GetComponent<NetworkEvents>();
            events.PlayerJoined.AddListener(PlayerJoined);
            events.OnConnectedToServer.AddListener(SpawnNetworkPlayer);
            events.PlayerLeft.AddListener(OnPlayerLeft);

            _spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;

        }
        public void SpawnNetworkPlayer(NetworkRunner runner)
        {
            if (runner.GameMode == GameMode.Shared || runner.GameMode == GameMode.Host)
            {

                SpawnPlayer(runner, runner.LocalPlayer);

            }

        }
        public void PlayerJoined(NetworkRunner runner, PlayerRef player)
        {

            SpawnPlayer(runner, player);

        }
        void SpawnPlayer(NetworkRunner runner, PlayerRef playerref)
        {

            if (runner.IsServer)
            {

                NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, _spawnPoint.position, Quaternion.identity, playerref);

                _spawnedCharacters.Add(playerref, networkPlayerObject);

            }

        }
        void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
            }
        }


    }
}