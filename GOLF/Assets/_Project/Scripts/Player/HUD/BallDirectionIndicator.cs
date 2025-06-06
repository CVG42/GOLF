using UnityEngine;

namespace Golf
{
    public class BallDirectionIndicator : MonoBehaviour
    {
        [SerializeField] LineRenderer _lineRenderer;
        [SerializeField] private Transform _ballTransform;
        [SerializeField] private float _lineRendererLength = 2f;

        private void Start()
        {
            InputManager.Source.OnActionChange += OnActionChange;
            InputManager.Source.OnDirectionChange += UpdateDirectionIndicator;
            OnActionChange(InputManager.Source.CurrentActionState);
        }
        
        private void OnDestroy()
        {
            InputManager.Source.OnActionChange -= OnActionChange;
            InputManager.Source.OnDirectionChange -= UpdateDirectionIndicator;
        }

        private void OnActionChange(ActionState actionState)
        {
            switch (actionState)
            {
                case ActionState.Direction:
                case ActionState.Force:
                    _lineRenderer.enabled = true;
                    break;
                default:
                    _lineRenderer.enabled = false;
                    break;
            }
        }

        private void UpdateDirectionIndicator(float angle)
        {
            Vector3 startPosition = _ballTransform.position;
            float radians = angle * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
            _lineRenderer.SetPosition(0, startPosition);
            _lineRenderer.SetPosition(1, startPosition + direction * _lineRendererLength);
        }
    }
}
