using UnityEngine;

namespace Golf
{
    public class BallPowerUp : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _ballSprite;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            PowerUpSystem.Source.OnPowerUpSelected += ApplyPowerUp;
        }

        private void OnDestroy()
        {
            PowerUpSystem.Source.OnPowerUpSelected -= ApplyPowerUp;
        }

        private void ApplyPowerUp(PowerUpData powerUpData)
        {
            _rigidbody.mass = powerUpData.BallMass;
            _rigidbody.angularDrag = powerUpData.BallAngularDrag;
            _ballSprite.sprite = powerUpData.BallSprite;
        }
    }
}
