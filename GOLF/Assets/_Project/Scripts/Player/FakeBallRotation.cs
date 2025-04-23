using UnityEngine;

namespace Golf
{
    public class FakeBallRotation : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _ballRigidbody;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private float _smoothness = 10;
        [SerializeField] private LayerMask _groundLayer;

        private float _currentRotation = 0;
        private float _targetRotation = 0;

        private void FixedUpdate()
        {
            if (InputManager.Source.CurrentActionState != ActionState.Moving) return;

            if (IsGrounded())
            {
                float velocity = _ballRigidbody.velocity.magnitude;
                _targetRotation -= velocity * _rotationSpeed;
            }

            _currentRotation = Mathf.Lerp(_currentRotation, _targetRotation, Time.fixedDeltaTime * _smoothness);
            transform.rotation = Quaternion.Euler(0, 0, _currentRotation);
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(_ballRigidbody.position, Vector2.down, 1f, _groundLayer);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_ballRigidbody != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(_ballRigidbody.position, _ballRigidbody.position + Vector2.down * 1f);
            }
        }
#endif
    }
}
