using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

namespace IronHead.MVRTFusion
{
    public class NetworkHelper : Fusion.Behaviour
    {

        public bool overrideAsSingle;
        public static Map map;

        public static int status = -1;
        public static SessionInfo session;


        private void Start()
        {
            if (overrideAsSingle)
            {
                status = -1;
                NetworkManager.Instance.Connect(GameMode.Single);
            }
            else
            {
                NetworkManager.Instance.Connect(GameMode.Host);
            }

            WaitForRunner();
        }
        public async void WaitForRunner()
        {
            while (NetworkManager.Instance.Runner == null)
            {
                await Task.Yield();
            }
            DoStart();

        }
        public void DEBUG(string debug)
        {
            Debug.Log(debug);
        }

        public void ChangeMap(GameMode gameMode, string sessionName, string mapName, int status)
        {
            map.gameMode = gameMode;
            map.sessionName = sessionName;
            map.mapName = mapName;
            Debug.Log(gameMode);
            ChangeStatus(status);


        }
        public void ChangeSession(SessionInfo info, int status)
        {
            session = info;
            ChangeStatus(status);
        }
        public void ChangeStatus(int i)
        {
            status = i;
            if (i == 1 || i == 2)
            {
                NetworkManager.Instance.Shutdown(map.mapName, GameMode.Host);
            }
            else
            {
                NetworkManager.Instance.Shutdown(map.mapName, GameMode.Single);
            }

        }
        void DoStart()
        {
            if (status == -1)
            {
                OverrideSingle();
            }
            else if (status == 0)
            {
                StartSingle();
            }
            else if (status == 1)
            {

                StartHost();
            }
            else if (status == 2)
            {
                StartClient();
            }
        }
        void OverrideSingle()
        {

            NetworkManager.Instance.HostSession(GameMode.Single, SceneManager.GetActiveScene().name, SceneManager.GetActiveScene().name);
        }
        void StartHost()
        {
            NetworkManager.Instance.HostSession(map.gameMode, map.sessionName, map.mapName);
        }
        void StartSingle()
        {

            NetworkManager.Instance.HostSession(map.gameMode, map.sessionName, SceneManager.GetActiveScene().name);
        }
        void StartClient()
        {
            NetworkManager.Instance.JoinSession(session);

        }
    }
}