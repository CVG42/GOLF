using UnityEngine;

namespace Golf
{
    public class BallController : MonoBehaviour
    {
        [Header("Physics parameters")]
        [Tooltip("Change the amount of force to be applied.")]
        [SerializeField] float force = 3f;
        [Tooltip("The maximum angle it can reach.")]
        [SerializeField] float maxAngle = 180f;
        [Tooltip("Change the speed of the angle setup.")]
        [SerializeField] float angleChangeSpeed = 50f;
        [Tooltip("Maximum distance it can reach.")]
        [SerializeField] float maxDistance = 5f;
        [Tooltip("Change the amount of drag to be applied so the ball stops sooner or later.")]
        [SerializeField] float dragAmount = 1f;

        private Rigidbody2D _rb;
        private float _currentAngle = 0f;
        private float _minRbVelocity = 0.01f;

        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            SelectAngle();
            ApplyForce();
            ApplyDrag();
        }

        /// <summary>
        /// Select the angle the ball will be thrown.
        /// </summary>
        void SelectAngle()
        {
            #region Generic inputs
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // move angle direction to the left.
                _currentAngle += angleChangeSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                // move angle direction the right.
                _currentAngle -= angleChangeSpeed * Time.deltaTime;
            }
            #endregion

            // Clamp current angle value so it doesn't surpass the limits.
            _currentAngle = Mathf.Clamp(_currentAngle, 0f, maxAngle);
        }

        /// <summary>
        /// Checks if the ball is moving.
        /// </summary>
        bool isMoving()
        {
            // If the velocity of the ball is greater than 0.01, return true.
            return _rb.velocity.magnitude > _minRbVelocity;
        }

        void ApplyForce()
        {
            if (!isMoving())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    float throwForce = Mathf.Clamp(Vector2.Distance(transform.position, Vector2.zero), 0, maxDistance) * force;

                    Vector2 throwDirection = GetDirection(_currentAngle);

                    _rb.AddForce(throwDirection * throwForce, ForceMode2D.Impulse);
                }
            }
        }

        /// <summary>
        /// Fuction to apply drag to the ball when it falls so it can stop.
        /// The amount of drag can be changed in the inspector.
        /// </summary>
        void ApplyDrag()
        {
            if (_rb.velocity.magnitude > 0)
            {
                _rb.velocity = _rb.velocity * (1f - dragAmount * Time.deltaTime);
            }

            // So it reaches zero completely
            if (_rb.velocity.magnitude < _minRbVelocity)
            {
                _rb.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// Function to get the direction of the angle in degrees converted from the original amount in radians.
        /// </summary>
        /// <param name="angleInDegrees"></param>
        /// <returns>Angle converted from radiants to degrees.</returns>
        Vector2 GetDirection(float angleInDegrees)
        {
            float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        }

        // Gizmo to see the throw direction in the editor.
        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Vector2 throwDirection = GetDirection(_currentAngle);

            float trajectoryLength = maxDistance;

            Gizmos.DrawLine(transform.position, (Vector2)transform.position + throwDirection * trajectoryLength);
        }
    }
}
