using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace IronHead.MVRTFusion
{
    public class VRPlayerUI : MonoBehaviour
    {


        [SerializeField]
        private TMP_Text fpsText;
        float time;
        int frameCount;
        float pollingTime = 1f;


        public void ReturnHomeScene()
        {
            NetworkManager.Instance.GetComponent<NetworkHelper>().ChangeMap(Fusion.GameMode.Single, "HomeScene", "HomeScene", 0);
        }

        void Update()
        {
            time += Time.deltaTime;
            frameCount++;
            if (time > pollingTime)
            {
                int frameRate = Mathf.RoundToInt(frameCount / time);
                fpsText.text = frameRate.ToString();
                time -= pollingTime;
                frameCount = 0;
            }
        }
    }
}
