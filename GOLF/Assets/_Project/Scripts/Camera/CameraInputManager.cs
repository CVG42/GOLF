using DG.Tweening;
using UnityEngine;

namespace Golf
{
    public class CameraInputManager : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _movementFrame;
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraInputConfigurations _cameraInputConfigurations;
        
        private const float _smoothTime = 0.1f;
        private bool _isLocking = true;
        private Vector2 _moveDirection = Vector2.zero;
        private Vector3 _velocityCamera = Vector3.zero;

        private void OnEnable()
        {
            InputManager.Source.OnToggleCameraMode += LockCamera;
        }

        private void OnDisable()
        {
            InputManager.Source.OnToggleCameraMode -= LockCamera;
            InputManager.Source.OnMoveCamera -= CameraDirection;
        }

        private void FixedUpdate()
        {
            if (_isLocking)
            {
                FollowPlayer();
            }
        }

        private void Update()
        {
            if (!_isLocking) 
            {
                MoveCamera();
            }
        }

        private void FollowPlayer()
        {
            Vector3 _playerPosition = _player.position + new Vector3(0f, 1f, -10f);

            _playerPosition.x = Mathf.Clamp(_playerPosition.x, _cameraInputConfigurations._xOffset_Negative, _cameraInputConfigurations._xOffset_Positive);
            _playerPosition.y = Mathf.Clamp(_playerPosition.y, _cameraInputConfigurations._yOffset_Negative, _cameraInputConfigurations._yOffset_Positive);

            transform.position = Vector3.SmoothDamp(transform.position, _playerPosition, ref _velocityCamera, _smoothTime);

            ZoomIn();
        }

        private void MoveCamera()
        {
            Vector3 _move = 20f * Time.deltaTime * (Vector3)_moveDirection;
            transform.position += _move;

            Vector3 _Threshold = transform.position + new Vector3(_move.x, _move.y, 0);

            _Threshold.x = Mathf.Clamp(_Threshold.x, _cameraInputConfigurations._zXOffset_Negative, _cameraInputConfigurations._zXOffset_Positive);
            _Threshold.y = Mathf.Clamp(_Threshold.y, _cameraInputConfigurations._zYOffset_Negative, _cameraInputConfigurations._zYOffset_Positive);
            transform.position = _Threshold;

            ZoomOut();
        }

        private void ZoomIn() 
        {
            _movementFrame.SetActive(false);
            _camera.DOOrthoSize(7, 0.3f).SetEase(Ease.OutQuad);
        }

        private void ZoomOut()
        {
            _movementFrame.SetActive(true);
            _camera.DOOrthoSize(10,1f).SetEase(Ease.OutQuad);
        }

        private void LockCamera(bool state)
        {
            _isLocking = state;
            if (!_isLocking)
            {
                InputManager.Source.OnMoveCamera += CameraDirection;
            }
            else
            {
                InputManager.Source.OnMoveCamera -= CameraDirection;
            }
        }

        private void CameraDirection(Vector2 direction)
        {
            _moveDirection = direction;
        }
    }

}
