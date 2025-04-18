using UnityEngine;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class BallController : MonoBehaviour
    {
        private const float MINIMUM_VELOCITY = 0.1f;

        private bool _isOnPole;
        private Vector2 _currentLastPosition;

        private float _stopTimer = 0f;
        private float _stopTimeRequired = 1f;
        private Rigidbody2D _rigidbody;
        private IInputSource _inputSource;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentLastPosition = transform.position;           
        }

        private void Start()
        {
            _isOnPole = false;
            GameManager.Source.OnBallRespawn += ResetBallLastPosition;
            InputManager.Source.OnLaunch += LaunchBall;        
        }

        private void OnDestroy()
        {
            GameManager.Source.OnBallRespawn -= ResetBallLastPosition;
            InputManager.Source.OnLaunch -= LaunchBall;
        }
        
        private void Update()
        {
            StopBallCheck();
        }

        private void LaunchBall(float angle, float force)
        {
            float radians = angle * Mathf.Deg2Rad;
            Vector2 launchDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            _rigidbody.AddForce(launchDirection * force, ForceMode2D.Impulse);
            GameManager.Source.ReduceHitsLeft();
        }

        private void StopBallCheck()
        {
            if (_inputSource.CurrentActionState != ActionState.Moving) return;
            
            if (_rigidbody.velocity.magnitude < MINIMUM_VELOCITY)
            {
                _stopTimer += Time.deltaTime;

                if (_stopTimer >= _stopTimeRequired)
                {
                    if (!_isOnPole && GameManager.Source.CurrentHitsLeft == 0)
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
            _stopTimer = 0f;
            _currentLastPosition = transform.position;
            _inputSource.ChangeAction(ActionState.Direction);
        }

        private void ResetBallLastPosition()
        {
            transform.position = _currentLastPosition;
            _rigidbody.velocity = Vector3.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hole"))
            {
                _isOnPole = true;
            }
        }
    }
}
