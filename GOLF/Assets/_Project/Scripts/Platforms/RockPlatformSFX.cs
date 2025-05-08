using UnityEngine;

namespace Golf
{
    public class RockPlatformSFX : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("RockPlatformSFX");
            }
        }
    }
}
