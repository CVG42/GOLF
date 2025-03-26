using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class BallForceSlider : MonoBehaviour
    {
        [SerializeField] private Slider _forceSlider;

        private void Awake()
        {
            _forceSlider.gameObject.SetActive(false);
        }

        private void Start()
        {
            InputManager.Source.OnActionChange += OnActionChange;
            InputManager.Source.OnForceChange += SetForce;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnActionChange -= OnActionChange;
            InputManager.Source.OnForceChange -= SetForce;
        }

        private void OnActionChange(ActionState actionState)
        {
            switch (actionState)
            {
                case ActionState.Force:
                    _forceSlider.gameObject.SetActive(true);
                    break;
                default:
                    _forceSlider.gameObject.SetActive(false);
                    break;
            }
        }

        private void SetForce(float force)
        {
            _forceSlider.value = force;
        }
    }
}
