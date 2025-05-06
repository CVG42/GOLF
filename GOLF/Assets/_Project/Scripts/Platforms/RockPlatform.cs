using UnityEngine;

namespace Golf
{
    public class RockPlatform : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("RockPlatformSFX");
            }
        }
    }
}
