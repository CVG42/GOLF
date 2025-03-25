using UnityEngine;
using System;

namespace Golf
{
    [Serializable]
    public class GameData
    {
        public float PlayerVolume;
        public bool IsFullScreen = true;
        public int ScreenWidth = 1920; 
        public int ScreenHeight = 1080;
    }
}
