using UnityEngine;

namespace Golf
{
    public class MainMenuBack : MonoBehaviour
    {
        private bool _isOnTrigger = false;
        private IInputSource _inputSource;

        private void Awake()
        {
            _inputSource = InputManager.Source;
        }

        private void OnDestroy()
        {
            _inputSource.OnConfirmButtonPressed -= BackToMenu;
        }

        private void BackToMenu()
        {
            LevelManager.Source.LoadScene("MainMenu");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && _isOnTrigger == false)
            {
                _inputSource.OnConfirmButtonPressed += BackToMenu;
                _isOnTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _inputSource.OnConfirmButtonPressed -= BackToMenu;
            _isOnTrigger = false;
        }
    }
}
