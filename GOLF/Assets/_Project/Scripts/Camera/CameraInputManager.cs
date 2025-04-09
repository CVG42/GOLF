using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Golf
{
    public class CameraInputManager : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _movementFrame;
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraInputConfigurations _cameraInputConfigurations;
        [SerializeField] private float _followSmoothTime = 0.1f;
        [SerializeField] private float _zoomTime = 0.4f;
        
        private bool _isLocked = true;
        private bool _enableFollow = true;
        private Vector3 _velocityCamera = Vector3.zero;
        private Sequence _currentTweenSequence;

        private void Start()
        {
            InputManager.Source.OnToggleCameraMode += ToggleLockCamera;
            GameManager.Source.OnBallRespawn += RepositionToPlayerAfterOneFrame;
        }

        private void OnDestroy()
        {
            InputManager.Source.OnToggleCameraMode -= ToggleLockCamera;
            InputManager.Source.OnMoveCamera -= MoveCamera;
            GameManager.Source.OnBallRespawn -= RepositionToPlayerAfterOneFrame;
        }

        private void FixedUpdate()
        {
            if (_enableFollow)
            {
                FollowPlayer();
            }
        }

        private void ToggleLockCamera(bool state)
        {
            _isLocked = state;
            if (!_isLocked)
            {
                ZoomOutToCameraMode();
            }
            else
            {
                ZoomInToFollowPlayer();
            }
        }

        private void RepositionToPlayerAfterOneFrame()
        {
            Reposition();
            
            async void Reposition()
            {
                await UniTask.DelayFrame(1);
                transform.position = GetClampedPlayerModePosition(GetFollowPlayerPosition());
            }
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.SmoothDamp(transform.position, GetFollowPlayerPosition(), ref _velocityCamera, _followSmoothTime);
            transform.position = GetClampedPlayerModePosition(transform.position);
        }
        
        private Vector3 GetFollowPlayerPosition()
        {
            return _player.position + new Vector3(0f, 1f, -10f);;
        }

        private Vector3 GetClampedPlayerModePosition(Vector3 position)
        {
            var clampedPosition = position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _cameraInputConfigurations.XOffsetNegative, _cameraInputConfigurations.XOffsetPositive);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _cameraInputConfigurations.YOffsetNegative, _cameraInputConfigurations.YOffsetPositive);
            
            return clampedPosition;
        }

        private void MoveCamera(Vector2 moveDirection)
        {
            Vector3 move = 20f * Time.deltaTime * (Vector3)moveDirection;
            transform.position += move;
            transform.position = GetClampedCameraModePosition(transform.position);
        }

        private Vector3 GetClampedCameraModePosition(Vector3 position)
        {
            var clampedPosition = position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _cameraInputConfigurations.ZXOffsetNegative, _cameraInputConfigurations.ZXOffsetPositive);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _cameraInputConfigurations.ZYOffsetNegative, _cameraInputConfigurations.ZYOffsetositive);
            
            return clampedPosition;
        }

        private void ZoomInToFollowPlayer() 
        {
            InputManager.Source.OnMoveCamera -= MoveCamera;
            
            _movementFrame.SetActive(false);
            _currentTweenSequence?.Kill();

            _currentTweenSequence = DOTween.Sequence();
            _currentTweenSequence.Append(_camera.DOOrthoSize(7, _zoomTime).SetEase(Ease.OutQuad));
            _currentTweenSequence.Join(transform.DOMove(GetClampedPlayerModePosition(GetFollowPlayerPosition()), _zoomTime).SetEase(Ease.OutQuad));
            _currentTweenSequence.AppendCallback(() => _enableFollow = true);
        }

        private void ZoomOutToCameraMode()
        {
            _enableFollow = false;
            _movementFrame.SetActive(true);
            _currentTweenSequence?.Kill();
            
            _currentTweenSequence = DOTween.Sequence();
            _currentTweenSequence.Append(_camera.DOOrthoSize(10, _zoomTime).SetEase(Ease.OutQuad));
            _currentTweenSequence.Join(transform.DOMove(GetClampedCameraModePosition(GetFollowPlayerPosition()), _zoomTime).SetEase(Ease.OutQuad));
            _currentTweenSequence.AppendCallback(() => InputManager.Source.OnMoveCamera += MoveCamera);
        }
    }
}
