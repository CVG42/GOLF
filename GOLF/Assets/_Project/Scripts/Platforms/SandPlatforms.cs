using UnityEngine;

namespace Golf
{
    public class SandPlatforms : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity/6;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = collision.gameObject.GetComponent<Rigidbody2D>().velocity/6;
        }
    }
}
