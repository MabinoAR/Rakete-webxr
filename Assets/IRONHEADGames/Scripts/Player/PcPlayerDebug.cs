using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronHead.MVRTFusion
{
    public class PcPlayerDebug : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                NetworkManager.Instance.GetComponent<NetworkHelper>().ChangeMap(Fusion.GameMode.Single, "HomeScene", "HomeScene", 0);
            }
        }
    }
}