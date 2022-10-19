using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public struct InputData : INetworkInput
    {

        public Vector3 HeadLocalPosition;
        public Quaternion HeadLocalRotation;

        public InputDataController Left;
        public InputDataController Right;


        public Vector2 move;
        public Vector2 look;
    }

    public struct InputDataController : INetworkStruct
    {

        public Vector3 LocalPosition;
        public Quaternion LocalRotation;

        public Vector2 Movement;
        public Vector2 Move;
        public Vector2 Rotate;


    }
    public struct NetworkPcInput : INetworkInput
    {
        public Vector2 move;
        public Vector2 look;
    }
}