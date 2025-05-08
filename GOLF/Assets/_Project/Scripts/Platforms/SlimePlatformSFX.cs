using UnityEngine;

namespace Golf
{
    public class SlimePlatformSFX : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("SlimePlatformSFX");
            }
        }
    }
}
