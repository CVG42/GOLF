using UnityEngine;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class BallController : MonoBehaviour
    {
        private const float MINIMUM_VELOCITY = 0.1f;

        [SerializeField] private float _angularVelocity;
        [SerializeField] private float _angularDrag;
        [SerializeField] private LayerMask _groundLayer;

        private bool _isOnPole;
        private Vector2 _currentLastPosition;

        private float _stopTimer = 0f;
        private float _stopTimeRequired = 1f;
        private Rigidbody2D _rigidbody;
        private IInputSource _inputSource;

        private Vector2 _savedVelocity;
        private float _savedAngularVelocity;
        private int _onAirThrows = 1;

        private void Awake()
        {
            _inputSource = InputManager.Source;
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentLastPosition = transform.position;
        }

        private void Start()
        {
            _isOnPole = false;
            GameStateManager.Source.OnGameStateChanged += OnGameStatedChanged;
            GameManager.Source.OnBallRespawn += ResetBallLastPosition;
            InputManager.Source.OnLaunch += LaunchBall;
            InputManager.Source.OnPowerUpActivated += ApplyCurrentPowerUpEffect;
        }

        private void OnDestroy()
        {
            GameManager.Source.OnBallRespawn -= ResetBallLastPosition;
            InputManager.Source.OnLaunch -= LaunchBall;
            GameStateManager.Source.OnGameStateChanged -= OnGameStatedChanged;
            InputManager.Source.OnPowerUpActivated -= ApplyCurrentPowerUpEffect;
        }

        private void Update()
        {
            if (GameStateManager.Source.CurrentGameState == GameState.OnPause) return;
            
            StopBallCheck();
            SetAngularDrag();
        }

        private void LaunchBall(float angle, float force)
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 launchDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
            _rigidbody.AddForce(launchDirection * force, ForceMode2D.Impulse);
            AddTorque(launchDirection.x);
            GameManager.Source.ReduceHitsLeft();
            AudioManager.Source.PlayOneShot("BallHitSFX");

            if (IsGrounded())
            {
                PowerUpSystem.Source?.StrokesCooldown();
            }
            
            if (PowerUpSystem.Source != null)
            {
                PowerUpSystem.Source.IsEffectActivated = false;
            }
        }

        private void AddTorque(float x)
        { 
            _rigidbody.AddTorque(-x * _angularVelocity);
        }

        private void SetAngularDrag()
        {
            if (IsGrounded())
            {
                _rigidbody.angularDrag = _angularDrag;
            }
            else
            {
                _rigidbody.angularDrag = 0;
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.Raycast(_rigidbody.position, Vector2.down, 1f, _groundLayer);
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
            
            if (!_isOnPole && GameManager.Source.CurrentHitsLeft != 0)
            {
                _inputSource.ChangeAction(ActionState.Direction);
                _onAirThrows = 1;
            }
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

        private void OnGameStatedChanged(GameState state)
        {
            switch (state)
            {
                case GameState.OnPlay:
                    ResumePhysics();
                    break;
                case GameState.OnPause:
                    PausePhysics();
                    break;
            }
        }

        private void ApplyCurrentPowerUpEffect()
        {
            if (!PowerUpSystem.Source.TryActivatePowerUp()) return;

            var currentData = PowerUpSystem.Source.CurrentPowerUpData;
            
            if (currentData == null) return;

            switch (currentData.PowerUpType)
            {
                case PowerUpType.Ice:
                    ApplyIceEffect();
                    break;
                default:
                    break;
            }
        }

        private void ApplyIceEffect()
        {
            if (!IsGrounded() && _onAirThrows > 0)
            {
                PowerUpSystem.Source.IsEffectActivated = true;
                _onAirThrows--;
                PausePhysics();
                _inputSource.ChangeAction(ActionState.Direction);
            }
        }
        
        private void PausePhysics()
        {
            _savedVelocity = _rigidbody.velocity;
            _savedAngularVelocity = _rigidbody.angularVelocity;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        private void ResumePhysics()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;

            _rigidbody.velocity = _savedVelocity;
            _rigidbody.angularVelocity = _savedAngularVelocity;
        }
    }
}
