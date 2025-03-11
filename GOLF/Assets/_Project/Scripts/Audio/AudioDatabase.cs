using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Scriptables/AudioDatabase", order = 0)]
    public class AudioDatabase : ScriptableObject
    {
        [SerializeField] AudioData[] _audioData;
        private Dictionary<string, AudioData> _audioDataDictionary = new Dictionary<string, AudioData>();

        private void OnEnable()
        {
            _audioDataDictionary = _audioData.ToDictionary(audioData => audioData.audioName, audioData => audioData);  
        }

        public AudioClip GetAudio(string _audioName)
        {
            if (_audioDataDictionary.TryGetValue(_audioName, out AudioData audioData)) return audioData.audio;

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
