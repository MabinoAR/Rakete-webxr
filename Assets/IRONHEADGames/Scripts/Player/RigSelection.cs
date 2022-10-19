using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public class RigSelection : SimulationBehaviour, ISpawned
    {
        Rect panelRect;
        static bool showUI = false;

        [SerializeField] Camera _cam;
        [SerializeField] GameObject _XRRig;
        [Space]
        [SerializeField] GameObject _PCRig;
        static bool SavedSpawnSelectionXR = false;
        static bool SavedSpawnSelectionPC = true;



#if UNITY_EDITOR
        void Start()
        {
            panelRect = new Rect(Screen.width / 2 - 50, 250, 200, 100);
        }
#else
        void Awake()
        {
        // SavedSpawnSelectionXR = true;
            ActivateRig(_PCRig);
        }

#endif
        void ISpawned.Spawned()
        {


            if (Object.HasInputAuthority)
            {


                if (Application.platform == RuntimePlatform.Android || SavedSpawnSelectionXR)
                {
                    ActivateRig(_XRRig);
                }
                else if (SavedSpawnSelectionPC)
                {
                    ActivateRig(_PCRig);
                }


                GetComponent<PlayerObject>().SetUpPlayer();

                LocalPlayerData.LocalPlayer = gameObject;


            }
            else
            {
                GetComponent<PlayerObject>().SetClient();
            }



        }


        private void OnGUI()
        {
            if (showUI)
            {
                panelRect = GUI.Window(3, panelRect, PanelFunc, "RigSelection");
            }

        }
        void PanelFunc(int id)
        {


            GUILayout.BeginVertical();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("PC_Rig"))
                {
                    SavedSpawnSelectionPC = true;
                    ActivateRig(_PCRig);
                    SetCursorLocked();
                }
                if (GUILayout.Button("VR_Rig"))
                {
                    SavedSpawnSelectionXR = true;
                    ActivateRig(_XRRig);
                }
                GUILayout.FlexibleSpace();
            }
            GUILayout.EndVertical();
        }

        void ActivateRig(GameObject rig)
        {
            ObserverCamera.DisableObserverCamera();
            showUI = false;

            rig.SetActive(true);
            LocalPlayerData.PlayerCamera = rig.GetComponentInChildren<Camera>();
            if (rig == _XRRig)
            {

                Destroy(_PCRig);

            }
            else
            {
                Destroy(_XRRig);

            }

            this.enabled = false;
        }
        void SetCursorLocked()
        {

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDestroy()
        {

            Cursor.lockState = CursorLockMode.None;
        }
    }
}