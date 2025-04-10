using UnityEngine;

namespace Golf
{
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private int _levelID;
        [SerializeField] private string _sceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SaveSystem.Source.SetLevelCleared(_levelID);
                LevelManager.Source.LoadScene(_sceneName);
            }
        }
    }
}
