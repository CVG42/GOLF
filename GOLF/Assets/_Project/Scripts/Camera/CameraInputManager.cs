using System;
using UnityEngine;

namespace Golf
{
    public class CameraInputManager : MonoBehaviour
    {
        [SerializeField] private ACTUAL_SCENE _Scene;
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _movementFrame;
        [SerializeField] private Camera _camera;
        
        private float _xOffset_Positive, _xOffset_Negative, _yOffset_Positive, _yOffset_Negative, _smoothTime = 0.1f;
        private bool _isLocking = true;
        private Vector3 _velocityCamera = Vector3.zero;

        private void Start()
        {
            switch (_Scene) { 
                case ACTUAL_SCENE.Tutorial:
                    _xOffset_Negative = -2.5f;
                    _xOffset_Positive = 1f;
                    _yOffset_Negative = 2.5f;
                    _yOffset_Positive = 2.8f;
                break;
                case ACTUAL_SCENE.Level1:
                    _xOffset_Negative = -15f;
                    _xOffset_Positive = 15f;
                    _yOffset_Negative = 2f;
                    _yOffset_Positive = 67f;
                break;
                case ACTUAL_SCENE.Level2:
                    _xOffset_Negative = -20.5f;
                    _xOffset_Positive = 20.5f;
                    _yOffset_Negative = -0.5f;
                    _yOffset_Positive = 70f;
                    break;
                case ACTUAL_SCENE.Level3:
                    _xOffset_Negative = -20.5f;
                    _xOffset_Positive = 20.5f;
                    _yOffset_Negative = -0.5f;
                    _yOffset_Positive = 70f;
                    break;
                case ACTUAL_SCENE.Level4:
                    _xOffset_Negative = -20.5f;
                    _xOffset_Positive = 20.5f;
                    _yOffset_Negative = -0.5f;
                    _yOffset_Positive = 70f;
                    break;
                case ACTUAL_SCENE.Level5:
                    _xOffset_Negative = -20.5f;
                    _xOffset_Positive = 20.5f;
                    _yOffset_Negative = -0.5f;
                    _yOffset_Positive = 70f;
                    break;
            }
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
            if (Input.GetKeyDown(KeyCode.Y))
            {
                CenterToPlayer();
            }

            if (Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.S))
            {
                MoveCamera();
            }
        }

        private void CenterToPlayer()
        {
            Vector3 _playerPosition = new Vector3((int)_player.position.x, (int)_player.position.y, 0) + new Vector3(0, 1f, -10);

            _playerPosition.x = Mathf.Clamp(_playerPosition.x, _xOffset_Negative, _xOffset_Positive);
            _playerPosition.y = Mathf.Clamp(_playerPosition.y, _yOffset_Negative, _yOffset_Positive);

            transform.position = _playerPosition;

            _isLocking = true;
           
            _movementFrame.SetActive(false);
        }

        private void FollowPlayer()
        {
            Vector3 _playerPosition = _player.position + new Vector3(0f, 1f, -10f);

            _playerPosition.x = Mathf.Clamp(_playerPosition.x, _xOffset_Negative, _xOffset_Positive);
            _playerPosition.y = Mathf.Clamp(_playerPosition.y, _yOffset_Negative, _yOffset_Positive);

            transform.position = Vector3.SmoothDamp(transform.position, _playerPosition, ref _velocityCamera, _smoothTime);


            _movementFrame.SetActive(false);
        }

        private void MoveCamera()
        {
            float _horizontalInput = Input.GetAxis("Horizontal") * 20f * Time.deltaTime;
            float _verticalInput = Input.GetAxis("Vertical") * 20f * Time.deltaTime;

            Vector3 _Threshold = transform.position + new Vector3(_horizontalInput, _verticalInput, 0);

            _Threshold.x = Mathf.Clamp(_Threshold.x, _xOffset_Negative, _xOffset_Positive);
            _Threshold.y = Mathf.Clamp(_Threshold.y, _yOffset_Negative, _yOffset_Positive);

            transform.position = _Threshold;

            _isLocking = false;
            _movementFrame.SetActive(true);
        }
    }

}

public enum ACTUAL_SCENE
{
    Tutorial,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}