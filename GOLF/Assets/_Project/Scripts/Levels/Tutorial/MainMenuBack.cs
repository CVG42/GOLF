using UnityEngine;

namespace Golf
{
    public class MainMenuBack : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.A))
            {
                LevelManager.Source.LoadScene("MainMenu");
            }
        }
    }
}
