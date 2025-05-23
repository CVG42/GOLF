using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/Powerup Data")]
    public class PowerUpData : ScriptableObject
    {
        [SerializeField] private PowerUpType _powerUpType;
        [SerializeField] private float _ballMass;
        [SerializeField] private float _ballAngularDrag;
        [SerializeField] private Sprite _ballSprite;
        [SerializeField] private int _hardness;
        [SerializeField] private bool _hasEffect;

        public PowerUpType PowerUpType => _powerUpType;
        public float BallMass => _ballMass;
        public float BallAngularDrag => _ballAngularDrag;
        public Sprite BallSprite => _ballSprite;
        public int Hardness => _hardness;
        public bool HasEffect => _hasEffect;
    }

    public enum PowerUpType
    {
        None,
        Ice,
        Fire,
        Stone,
    }
}
