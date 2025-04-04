using UnityEngine;

namespace Golf
{
    public class WaterPlatforms : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {                
                GameManager.Source.RespawnLastPosition();
            }
        }
    }
}
