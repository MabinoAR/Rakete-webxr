using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronHead.MVRTFusion
{
    public class ObserverCamera : MonoBehaviour
    {
        static GameObject Instance;
        // Start is called before the first frame update
        public static void DisableObserverCamera()
        {
            if (Instance != null)
            {
                Instance.SetActive(false);
            }
        }
        private void Awake()
        {
            Instance = gameObject;


            if (Application.platform == RuntimePlatform.Android)
            {
                DisableObserverCamera();
            }
        }
    }
}