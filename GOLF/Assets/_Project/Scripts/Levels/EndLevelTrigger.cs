using UnityEngine;

namespace Golf
{
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private int _levelID;
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _isTutorialLevel = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (!_isTutorialLevel)
                {
                    SaveSystem.Source.SetLevelCleared(_levelID);
                }
                else
                {
                    SaveSystem.Source.MarkTutorialAsCleared();
                }

                AudioManager.Source.FadeOutMusic();
                LevelManager.Source.LoadScene(_sceneName);
            }
        }
    }
}
