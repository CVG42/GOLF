using UnityEngine;
using DG.Tweening;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class BallController : MonoBehaviour
    {
        [SerializeField] private Transform _transition;

        private const float MINIMUM_VELOCITY = 0.1f;

        public bool isOnPole;
        public Vector2 _currentLastPosition;

        private float _stopTimer = 0f;
        private float _stopTimeRequired = 1f;
        private Rigidbody2D _rigidbody;
        private IInputSource _inputSource;
        private Sequence _currentTweenSequence;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentLastPosition = transform.position;           
        }

        private void OnEnable()
        {
            InputManager.Source.OnLaunch += LaunchBall;        
        }

        private void OnDisable()
        {
            InputManager.Source.OnLaunch -= LaunchBall;
        }

        private void Start()
        {
            isOnPole = false;
            GameManager.Source.OnBallRespawn += RespawnBall;
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
            _stopTimer = 0f;
            _currentLastPosition = transform.position;
            _inputSource.ChangeAction(ActionState.Direction);
        }

        public void RespawnBall()
        {
            _currentTweenSequence?.Kill();
            _currentTweenSequence = DOTween.Sequence();
            _currentTweenSequence.Append(_transition.DOLocalMoveX(0, 1f, true));
            _currentTweenSequence.AppendCallback(() => transform.position = _currentLastPosition);
            _rigidbody.velocity = Vector3.zero;
            _currentTweenSequence.Append(_transition.DOLocalMoveX(1920, 1f));
            _currentTweenSequence.AppendCallback(() => _transition.transform.position = new Vector2(-960, _transition.transform.position.y));
            GameManager.Source.OnBallRespawn -= RespawnBall;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Hole"))
            {
                isOnPole = true;
            }
            if (collision.CompareTag("Water"))
            {
                GameManager.Source.OnBallRespawn += RespawnBall;
            }
        }
    }
}
