using UnityEngine;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class BallController : MonoBehaviour
    {
        private const float MINIMUM_VELOCITY = 0.01f;
        
        [Header("Physics parameters")]
        [SerializeField] private float _force = 3f;
        [SerializeField] private float _maxDistance = 5f;
        [SerializeField] private float _dragAmount = 1f;

        private Rigidbody2D _rigidbody;
        private IInputSource _inputSource;
        
        private bool IsMoving => _rigidbody.velocity.magnitude > MINIMUM_VELOCITY;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _rigidbody = GetComponent<Rigidbody2D>();           
        }

        private void Start()
        {
            _inputSource.OnLaunchBall += LaunchBall;
        }

        private void Update()
        {
            ApplyDrag();
        }

        private void OnDestroy()
        {
            _inputSource.OnLaunchBall -= LaunchBall;
        }

        private void LaunchBall(Vector2 angleDirection)
        {
            if (IsMoving) return;
            
            var throwForce = Mathf.Clamp(Vector2.Distance(transform.position, Vector2.zero), 0, _maxDistance) * _force;

            _rigidbody.AddForce(angleDirection * throwForce, ForceMode2D.Impulse);
        }

        private void ApplyDrag()
        {
            if (_rigidbody.velocity.magnitude > 0)
            {
                _rigidbody.velocity *= (1f - _dragAmount * Time.deltaTime);
            }

            if (_rigidbody.velocity.magnitude < MINIMUM_VELOCITY)
            {
                _rigidbody.velocity = Vector2.zero;
            }
        }
    }

#if UNITY_EDITOR
    public partial class BallController
    {
        private void OnDrawGizmos()
        {
            if (_inputSource == null) return;
            
            var trajectoryLength = _maxDistance;
            var position = transform.position;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(position, (Vector2)position + _inputSource.GetCurrentAngleDirection() * trajectoryLength);
        }
    }
#endif
}
