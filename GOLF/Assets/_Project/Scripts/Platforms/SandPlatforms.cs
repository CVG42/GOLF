using UnityEngine;

namespace Golf
{
    public class SandPlatforms : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
