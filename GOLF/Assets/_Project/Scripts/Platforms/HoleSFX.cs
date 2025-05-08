using UnityEngine;

namespace Golf
{
    public class HolePlatformSFX : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("HoleSFX");
            }
        }
    }
}
