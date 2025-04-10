using UnityEngine;

namespace Golf
{
    public class LevelSelector : MonoBehaviour
    {
        [SerializeField] private GameObject _levelLayout;
        //[SerializeField] private GameObject _previouLevelLock;
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _isUnlocked;
        [SerializeField] private bool _isFirstLevel = false;

        private int _levelID;

        private void Start()
        {
            UpdateUnlockStatus();           
        }

        private void UpdateUnlockStatus()
        {
            if (_isFirstLevel) return;
            
            _levelID = int.Parse(gameObject.name) - 1;
            _isUnlocked = SaveSystem.Source.IsLevelUnlocked(_levelID);
            _levelLayout.SetActive(!_isUnlocked);
            //_previouLevelLock.SetActive(_isUnlocked);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelSelected(); 
            }
        }

        private void LevelSelected()
        {
            if (_isUnlocked || _isFirstLevel) 
            {
                LevelManager.Source.LoadScene(_sceneName);
            }
        }
    }
}
