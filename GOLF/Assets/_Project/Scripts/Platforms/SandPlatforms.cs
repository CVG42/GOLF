using UnityEngine;

namespace Golf
{
    public class SandPlatforms : MonoBehaviour
    {
        [SerializeField] private int _velocityReduction;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var collisionRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            collisionRigidbody.angularVelocity = collisionRigidbody.angularVelocity / _velocityReduction;
            collisionRigidbody.velocity = collisionRigidbody.velocity / _velocityReduction;
        }
    }
}
