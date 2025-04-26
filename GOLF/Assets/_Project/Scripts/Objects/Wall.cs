using UnityEngine;

namespace Golf
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Collider2D _wallCollider;
        [SerializeField] private int _wallHardness;

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
            _wallCollider.isTrigger = powerUpData.Hardness > _wallHardness;
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
