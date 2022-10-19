using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IronHead.MVRTFusion
{
    public class CharacterSelection : MonoBehaviour
    {
        private int _colorIndex = 0;
        private int _bodyIndex = 0;
        private int _headIndex = 0;

        [SerializeField] Renderer colorPalette;
        [SerializeField] GameObject bodyModel;
        [SerializeField] GameObject headModel;

        [SerializeField] PlayerCustomization playerCustomization;


        void SetColor()
        {
            Color currentColor = playerCustomization.Colors[_colorIndex];
            colorPalette.material.color = currentColor;
            bodyModel.GetComponent<MeshRenderer>().sharedMaterials[0].color = currentColor;
            headModel.GetComponent<MeshRenderer>().sharedMaterials[0].color = currentColor;

            LocalPlayerData.PlayerColor = _colorIndex;

            LocalPlayerData.LocalPlayer.GetComponent<PlayerObject>().SetUpPlayer();
        }
        void SetBody()
        {
            bodyModel.GetComponent<MeshFilter>().sharedMesh = playerCustomization.Bodies[_bodyIndex].GetComponent<MeshFilter>().sharedMesh;
            bodyModel.GetComponent<MeshRenderer>().sharedMaterials = playerCustomization.Bodies[_bodyIndex].GetComponent<MeshRenderer>().sharedMaterials;
            bodyModel.transform.localScale = playerCustomization.Bodies[_bodyIndex].transform.localScale;

            LocalPlayerData.PlayerBody = _bodyIndex;

            LocalPlayerData.LocalPlayer.GetComponent<PlayerObject>().SetUpPlayer();

        }
        void SetHead()
        {
            headModel.GetComponent<MeshFilter>().sharedMesh = playerCustomization.Heads[_headIndex].GetComponent<MeshFilter>().sharedMesh;
            headModel.GetComponent<MeshRenderer>().sharedMaterials = playerCustomization.Heads[_headIndex].GetComponent<MeshRenderer>().sharedMaterials;
            headModel.transform.localScale = playerCustomization.Heads[_headIndex].transform.localScale;

            LocalPlayerData.PlayerHead = _headIndex;

            LocalPlayerData.LocalPlayer.GetComponent<PlayerObject>().SetUpPlayer();

        }
        public void GetColor(int increment)
        {
            _colorIndex = GetIndex(playerCustomization.Colors.Count, _colorIndex, increment);

            SetColor();

        }
        public void GetBody(int increment)
        {
            _bodyIndex = GetIndex(playerCustomization.Bodies.Count, _bodyIndex, increment);
            SetBody();
        }
        public void GetHead(int increment)
        {
            _headIndex = GetIndex(playerCustomization.Heads.Count, _headIndex, increment);
            SetHead();
        }




        int GetIndex(int listCount, int index, int increment)
        {
            index = (index + increment) % listCount;
            if (index < 0)
            {
                index = listCount - 1;
            }
            return index;
        }
    }
}