using UnityEngine;

namespace Golf
{
    public class SlimePlatform : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("SlimePlatformSFX");
            }
        }
    }
}
