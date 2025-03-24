using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class BallController : MonoBehaviour
    {
        private const float MINIMUM_VELOCITY = 0.1f;

        public bool isOnPole;

        [Header("Physics parameters")]
        [SerializeField] private float _force = 3f;
        [SerializeField] private float _angle = 45f;
        [SerializeField] private float _directionChangeSpeed = 90f;
        [SerializeField] private float _forceSliderSpeed = 8f;
        [SerializeField] private Slider _forceSlider;
        [SerializeField] LineRenderer _lineRenderer;

        private float _lineRendererLenght = 2f;
        private float _forceTimer = 0f;
        private float _stopTimer = 0f;
        private float _stopTimeRequired = 1f;
        private Rigidbody2D _rigidbody;
        private IInputSource _inputSource;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            isOnPole = false;
            _forceSlider.gameObject.SetActive(false);
            if (_forceSlider != null)
            {
                _forceSlider.minValue = 1f;
                _forceSlider.maxValue = 10f;
            }
        }

        public void AdjustThrowDirection(float _input)
        {
            _angle -= _input * _directionChangeSpeed * Time.deltaTime;
            _angle = Mathf.Clamp(_angle, 0f, 180f);
        }

        public void UpdateDirectionIndicator()
        {
            if (_lineRenderer != null)
            {
                _lineRenderer.enabled = true;
                Vector3 startPosition = _rigidbody.transform.position;
                float radians = _angle * Mathf.Deg2Rad;
                Vector3 direction = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f);
                _lineRenderer.SetPosition(0, startPosition);
                _lineRenderer.SetPosition(1, startPosition + direction * _lineRendererLenght);
            }
        }

        public void ChangeToForceSelection()
        {
            _inputSource.ChangeAction(ACTION_STATE.FORCE);
            _forceTimer = 0f;
            if (_forceSlider != null)
            {
                _forceSlider.gameObject.SetActive(true);
            }
        }

        public void SetStrokeForce()
        {
            _forceTimer += Time.deltaTime;
            _force = Mathf.PingPong(_forceTimer * _forceSliderSpeed, 9f) + 1f;
            if (_forceSlider != null)
            {
                _forceSlider.value = _force;
            }
        }

        public void LaunchBall()
        {
            _inputSource.ChangeAction(ACTION_STATE.LAUNCH);
            if (_lineRenderer != null)
            {
                _lineRenderer.enabled = false;
            }
            if(_forceSlider != null)
            {
                _forceSlider.gameObject.SetActive(false);
            }

            float radians = _angle * Mathf.Deg2Rad;
            Vector2 launchDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            _rigidbody.AddForce(launchDirection * _force, ForceMode2D.Impulse);
            GameManager.Source.ReduceHitsLeft();
        }

        public void StopBallCheck()
        {
            if (_rigidbody.velocity.magnitude < MINIMUM_VELOCITY)
            {
                _stopTimer += Time.deltaTime;

                if (_stopTimer >= _stopTimeRequired)
                {
                    if (!isOnPole && GameManager.Source.CurrentHitsLeft == 0)
                    {
                        GameManager.Source.TriggerLoseCondition();
                    }

                    ResetBall();
                }
            }
            else
            {
                _stopTimer = 0f;
            }
        }

        private void ResetBall()
        {
            _force = 1f;
            _stopTimer = 0f;

            if (_forceSlider != null)
            {
                _forceSlider.gameObject.SetActive(false);
            }

            _inputSource.ChangeAction(ACTION_STATE.DIRECTION);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hole"))
            {
                isOnPole = true;
            }
        }
    }
}
