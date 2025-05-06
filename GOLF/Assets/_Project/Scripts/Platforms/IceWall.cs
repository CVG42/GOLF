using UnityEngine;

namespace Golf
{
    public class IceWall : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                AudioManager.Source.PlayOneShot("IceWallSFX");
            }
        }
    }
}
