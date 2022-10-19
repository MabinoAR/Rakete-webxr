using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

namespace IronHead.MVRTFusion
{
    public class PlayerData : MonoBehaviour
    {

        [SerializeField] TMP_Text PlayerNameText;

        [SerializeField] GameObject BodyMesh;
        [SerializeField] GameObject HeadMesh;
        [SerializeField] GameObject LeftHandMesh;
        [SerializeField] GameObject RightHandMesh;

        [SerializeField] PlayerCustomization playerCustomization;
        public void SetNetworkPlayer()
        {
            PlayerNameText.gameObject.SetActive(false);
        }
        public void SetColour(int index)
        {

            Debug.Log(NetworkManager.Instance.Runner.LocalPlayer.PlayerId + " :Local: color changed to: " + playerCustomization.Colors[index].ToString());

            BodyMesh.GetComponent<MeshRenderer>().materials[0].color = playerCustomization.Colors[index];
            HeadMesh.GetComponent<MeshRenderer>().materials[0].color = playerCustomization.Colors[index];
            LeftHandMesh.GetComponent<MeshRenderer>().materials[0].color = playerCustomization.Colors[index];
            RightHandMesh.GetComponent<MeshRenderer>().materials[0].color = playerCustomization.Colors[index];


        }
        public void SetBody(int index)
        {
            BodyMesh.GetComponent<MeshFilter>().sharedMesh = playerCustomization.Bodies[index].GetComponent<MeshFilter>().sharedMesh;
            BodyMesh.GetComponent<MeshRenderer>().sharedMaterials = playerCustomization.Bodies[index].GetComponent<MeshRenderer>().sharedMaterials;
            BodyMesh.transform.localScale = playerCustomization.Bodies[index].transform.localScale;


        }
        public void SetHead(int index)
        {
            HeadMesh.GetComponent<MeshFilter>().sharedMesh = playerCustomization.Heads[index].GetComponent<MeshFilter>().sharedMesh;
            HeadMesh.GetComponent<MeshRenderer>().sharedMaterials = playerCustomization.Heads[index].GetComponent<MeshRenderer>().sharedMaterials;
            HeadMesh.transform.localScale = playerCustomization.Heads[index].transform.localScale;


        }

        public void SetPlayerName(string playerName)
        {

            if (PlayerNameText != null)
            {
                PlayerNameText.text = playerName;
            }

        }
        public void SetUpClientPlayer()
        {
            BodyMesh.layer = 0;
            HeadMesh.layer = 0;
        }

    }

}
