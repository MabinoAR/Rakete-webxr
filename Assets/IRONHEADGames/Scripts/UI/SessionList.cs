using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class SessionList : MonoBehaviour
    {
        //List update states
        enum EListState { StartUpdate, FinishUpdate, Failed }
        Dictionary<EListState, string> ListState = new Dictionary<EListState, string>()
    {
        { EListState.StartUpdate,"List Updating..."},
        { EListState.FinishUpdate,"List Updated."},
        {EListState.Failed,"Something went wrong!" }
    };
        [SerializeField] private Text _listStateText;


        public GameObject SessionBar;
        public GameObject parentObject;




        [SerializeField] private SessionItem _sessionListItemPrefab;
        [SerializeField] private Text _error;

        private PlayMode _playMode;

        private void Awake()
        {
            var events = NetworkManager.Instance.Runner.GetComponent<NetworkEvents>();
            events.OnSessionListUpdate.AddListener(UpdateSessionList);


        }
        private void ClearList()
        {
            foreach (Transform child in parentObject.transform)
            {
                Debug.Log("Destroy: " + child.name);
                Destroy(child.gameObject);
            }

        }
        public void Populate(List<SessionInfo> sessions)
        {
            ChangeListState(EListState.StartUpdate);
            Debug.Log("SessionCount: " + sessions.Count);
            ClearList();
            GameObject newObj;
            foreach (SessionInfo info in sessions)
            {
                newObj = (GameObject)Instantiate(SessionBar, parentObject.transform);


                newObj.GetComponent<SessionItem>().UpdateItem(info, selectedSession => NetworkManager.Instance.GetComponent<NetworkHelper>().ChangeSession(selectedSession, 2));

            }

            ChangeListState(EListState.FinishUpdate);
        }
        public void RefreshButton()
        {

            Populate(sessionInfos);



        }
        public async void Activate()
        {


            await NetworkManager.Instance.JoinLobby();

            InvokeRepeating(nameof(RefreshButton), 1f, 5f);

        }
        public void Deactivate()
        {
            ClearList();

        }

        private void ChangeListState(EListState eListState)
        {
            ListState.TryGetValue(eListState, out string text);
            _listStateText.text = text;
        }

        public List<SessionInfo> sessionInfos = new List<SessionInfo>();
        public void UpdateSessionList(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            sessionInfos = sessionList;
            Debug.Log("OnSessionListUpdated");
        }

    }
}