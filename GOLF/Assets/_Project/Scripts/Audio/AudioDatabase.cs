using System;
using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Scriptables/AudioDatabase", order = 0)]
    public class AudioDatabase : ScriptableObject
    {
        [SerializeField] AudioData[] _audioData;

        public AudioClip GetAudio(string _audioName)
        {
            for (int i = 0; i < _audioData.Length; i++)
            {
                if (_audioData[i].audioName == _audioName)
                {
                    return _audioData[i].audio; 
                }
            }

            return null;
        }
    }

    [Serializable]
    public class AudioData
    {
        public AudioClip audio;
        public string audioName;
    }
}
