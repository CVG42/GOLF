using UnityEngine;

namespace Golf
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Collider2D _wallCollider;

        private void Start()
        {
            PowerUpSystem.Source.OnPowerUpSelected += SetColliderType;
        }

        private void OnDestroy()
        {
            PowerUpSystem.Source.OnPowerUpSelected -= SetColliderType;
        }

        private void SetColliderType(PowerUpData powerUpData)
        {
            _wallCollider.isTrigger = powerUpData.canDestroyWalls;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
