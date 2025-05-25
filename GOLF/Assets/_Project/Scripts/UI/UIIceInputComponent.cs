using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class UIIceInputComponent : MonoBehaviour
    {
        [SerializeField] private Image _inputImage;
        [SerializeField] private Sprite _keyboardInputSprite;
        [SerializeField] private Sprite _controllerInputSprite;
        [SerializeField] private Sprite _emptySprite;

        private bool _wasOnCooldown;
        private void Start()
        {
            PowerUpSystem.Source.OnPowerUpSelected += GetCurrentPowerUp;
            _inputImage.sprite = _emptySprite;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnControllerTypeChange -= UpdateComponentSprite;
            PowerUpSystem.Source.OnPowerUpSelected -= GetCurrentPowerUp;
        }

        private void Update()
        {
            bool isOnCooldown = PowerUpSystem.Source.IsOnCooldown;

            if (isOnCooldown != _wasOnCooldown)
            {
                _inputImage.color = isOnCooldown ? Color.gray : Color.white;
                _wasOnCooldown = isOnCooldown;
            }
        }

        private void UpdateComponentSprite(ControllerType type)
        {
            switch (type)
            {
                case ControllerType.Keyboard:
                    _inputImage.sprite = _keyboardInputSprite;
                    break;

                case ControllerType.Xbox:
                    _inputImage.sprite = _controllerInputSprite;
                    break;
            }
            _inputImage.preserveAspect = true;
        }

        private void GetCurrentPowerUp(PowerUpData powerUpData) 
        {
            InputManager.Source.OnControllerTypeChange -= UpdateComponentSprite;

            if (powerUpData.HasEffect)
            {
                InputManager.Source.OnControllerTypeChange += UpdateComponentSprite;
                UpdateComponentSprite(InputManager.Source.CurrentController);
            }
            else
            {
                _inputImage.sprite = _emptySprite;
            }
        }
    }
}
