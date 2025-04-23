using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/Powerup Data")]
    public class PowerUpData : ScriptableObject
    {
        public float ballMass;
        public float ballAngularDrag;
        public Sprite ballSprite;
        public bool canDestroyWalls;
    }
}
