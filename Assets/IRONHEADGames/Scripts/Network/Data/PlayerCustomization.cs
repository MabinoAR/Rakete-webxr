using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace IronHead.MVRTFusion
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerCustomizationScriptableObject", order = 1)]
    public class PlayerCustomization : ScriptableObject
    {
        public List<Color> Colors;

        public List<GameObject> Bodies;

        public List<GameObject> Heads;

        public List<GameObject> Hands_Right;

        public List<GameObject> Hands_Left;
    }
}