using UnityEngine;

namespace Golf
{
    public class LevelFiveTriggerSpawn : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private GameObject _backgroundMusic;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerDialogue();
            gameObject.SetActive(false);
        }
        private void TriggerDialogue()
        {
            GameStateManager.Source.ChangeState(GameState.OnDialogue);
            DialogueManager.Source.StartCinematicDialogue(_dialogue, ActivateMusicLevel);
        }

        private void ActivateMusicLevel()
        {
            _backgroundMusic.SetActive(true);
        }
    }
}
