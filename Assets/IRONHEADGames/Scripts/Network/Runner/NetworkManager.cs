using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace IronHead.MVRTFusion
{
    public class NetworkManager : MonoBehaviour
    {
        private NetworkRunner _runner;

        private NetworkSceneManagerBase _loader;
        private static NetworkManager _instance;



        [SerializeField] private GameObject runnerPrefab;




        public NetworkRunner Runner
        {
            get { return _runner; }

        }
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<NetworkManager>();
                return _instance;
            }

        }
        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            if (_instance != this)
            {
                Destroy(gameObject);
            }
            else if (_loader == null)
            {
                _loader = GetComponent<NetworkSceneManagerBase>();

                DontDestroyOnLoad(gameObject);


            }

        }


        public void Connect(GameMode gameMode)
        {
            if (_runner == null)
            {
                GameObject go = Instantiate(runnerPrefab);
                //    go.transform.SetParent(transform);
                _runner = go.GetComponent<NetworkRunner>();
                _runner.AddCallbacks(go.GetComponent<NetworkEvents>());
                Debug.Log("Connect: " + SceneManager.GetActiveScene().name);
            }

        }

        public async void HostSession(GameMode mode, string sessionName, string mapName)
        {


            var customProps = new Dictionary<string, SessionProperty>();
            if (mode == GameMode.Host)
            {
                customProps["mapName"] = (string)mapName;
            }


            _runner.ProvideInput = true;
            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = sessionName,
                SceneManager = _loader,
                SessionProperties = customProps,

            });



            ChangeMap(mapName);

        }


        public async void JoinSession(SessionInfo info)
        {

            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = GameMode.Client,
                SessionName = info.Name,
                SceneManager = _loader,
                DisableClientSessionCreation = true,
            });
        }

        public void ChangeMap(string map)
        {
            Debug.Log("Change Map");

            _runner.SetActiveScene(map);


        }

        public async Task JoinLobby()
        {

            await _runner.JoinSessionLobby(SessionLobby.ClientServer);
        }

        public async void Shutdown(string scene, GameMode gameMode)
        {

            Camera camera = LocalPlayerData.PlayerCamera;
            camera.cullingMask = 0;
            camera.clearFlags = CameraClearFlags.Color;
            camera.backgroundColor = Color.black;


            await Task.Delay(60);

            await _runner.Shutdown();
            Destroy(_runner);


            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);


            while (!asyncLoad.isDone)
            {
                await Task.Yield();
            }
            Connect(gameMode);
            _runner.SetActiveScene(scene);
            GetComponent<NetworkHelper>().WaitForRunner();

        }

        public void QuitLobby()
        {
            if (_runner != null)
            {
                _runner.Shutdown(true, ShutdownReason.GameClosed);
                _runner = null;

            }


        }



        public NetworkRunner.States GetRunnerState()
        {
            return _runner.State;
        }


    }
}