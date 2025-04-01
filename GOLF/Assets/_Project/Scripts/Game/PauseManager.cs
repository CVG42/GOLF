using UnityEngine;

namespace Golf
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenuUI;
        [SerializeField] private Rigidbody2D golfBallRigidbody;

        private Vector2 savedVelocity;
        private float savedAngularVelocity;
        private bool isPaused = false;
        private GameManager gameManager;
        private InputManager inputManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            inputManager = FindObjectOfType<InputManager>();

            if (gameManager != null)
            {
                gameManager.OnPause += PauseGame;
                gameManager.OnResume += ResumeGame;
                gameManager.OnRestart += RestartGame;
            }

            if (inputManager != null)
            {
                inputManager.OnPauseButtonPressed += TogglePause;
            }

            pauseMenuUI.SetActive(false);
        }

        private void OnDestroy()
        {
            if (gameManager != null)
            {
                gameManager.OnPause -= PauseGame;
                gameManager.OnResume -= ResumeGame;
                gameManager.OnRestart -= RestartGame;
            }

            if (inputManager != null)
            {
                inputManager.OnPauseButtonPressed -= TogglePause;
            }
        }

        private void TogglePause()
        {
            if (!isPaused)
                gameManager.PauseGame();
            else
                gameManager.ResumeGame();
        }

        private void PauseGame()
        {
            isPaused = true;

            savedVelocity = golfBallRigidbody.velocity;
            savedAngularVelocity = golfBallRigidbody.angularVelocity;

            golfBallRigidbody.velocity = Vector2.zero;
            golfBallRigidbody.angularVelocity = 0f;
            golfBallRigidbody.simulated = false;

            pauseMenuUI.SetActive(true);
        }

        private void ResumeGame()
        {
            isPaused = false;

            golfBallRigidbody.simulated = true;
            golfBallRigidbody.velocity = savedVelocity;
            golfBallRigidbody.angularVelocity = savedAngularVelocity;

            pauseMenuUI.SetActive(false);
        }

        private void RestartGame()
        {
            isPaused = false;
            pauseMenuUI.SetActive(false);
        }
    }
}
