using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

namespace IronHead.MVRTFusion
{
    public struct Map
    {
        public GameMode gameMode;
        public string sessionName;
        public string mapName;
        public Map(GameMode gameMode, string sessionName, string mapName)
        {
            this.gameMode = gameMode;
            this.sessionName = sessionName;
            this.mapName = mapName;
        }
    }
}