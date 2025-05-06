using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class UIInputComponent : MonoBehaviour
    {
        [SerializeField] private Image _inputImage;
        [SerializeField] private Sprite _keyboardInputSprite;
        [SerializeField] private Sprite _controllerInputSprite;

        private void Start()
        {
            InputManager.Source.OnControllerTypeChange += UpdateComponentSprite;

            UpdateComponentSprite(InputManager.Source.CurrentController);
        }

        private void OnDestroy()
        {
            InputManager.Source.OnControllerTypeChange -= UpdateComponentSprite;
        }

        private void UpdateComponentSprite(ControllerType type)
        {
            switch (type)
            {
                case ControllerType.Keyboard:
                    _inputImage.sprite = _keyboardInputSprite;
                    _inputImage.preserveAspect = true;
                    break;

                case ControllerType.Xbox:
                    _inputImage.sprite= _controllerInputSprite;
                    _inputImage.preserveAspect = true;
                    break;
            }
        }
    }
}
