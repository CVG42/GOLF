using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/Powerup Data")]
    public class PowerUpData : ScriptableObject
    {
        [SerializeField] private float _ballMass;
        [SerializeField] private float _ballAngularDrag;
        [SerializeField] private Sprite _ballSprite;
        [SerializeField] private bool _canDestroyWalls;

        public float BallMass => _ballMass;
        public float BallAngularDrag => _ballAngularDrag;
        public Sprite BallSprite => _ballSprite;
        public bool CanDestroyWalls => _canDestroyWalls;
    }
}
