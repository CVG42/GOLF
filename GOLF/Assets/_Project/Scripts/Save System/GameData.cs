using UnityEngine;
using System;

namespace Golf
{
    [Serializable]
    public class GameData
    {
        public bool IsFullScreen = true;
        public int ScreenWidth = 1920; 
        public int ScreenHeight = 1080;
        public float MusicVolume = 1.0f;
        public float SFXVolume = 1.0f;
    }
}
