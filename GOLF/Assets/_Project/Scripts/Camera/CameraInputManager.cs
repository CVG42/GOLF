using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Golf
{
    public class CameraInputManager : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _movementCameraFrame;
        [SerializeField] private CameraInputConfigurations _cameraInputConfigurations;
        [SerializeField] private float _followSmoothTime = 0.1f;
        [SerializeField] private GameObject[] _cameraArrows;

        private Vector3 _velocityCamera = Vector3.zero;

        private void OnEnable()
        {
            InputManager.Source.OnMoveCamera += MoveCamera;
        }
        private void OnDestroy()
        {
            InputManager.Source.OnMoveCamera -= MoveCamera;
        }

        private void FixedUpdate()
        {
            if (InputManager.Source.IsLocking == true)
            {
                FollowPlayer();
            }
        }

        private void Update()
        {
            if (CheckBallAction(InputManager.Source.CurrentActionState))
            {
                InputManager.Source.IsLocking = true;
                _movementCameraFrame.SetActive(false);
            }
            else
            {
                InputManager.Source.IsLocking = false;
                _movementCameraFrame.SetActive(true);
            }

            HideArrow();
        }

        private void FollowPlayer()
        {
            transform.position = Vector3.SmoothDamp(transform.position, GetFollowPlayerPosition(), ref _velocityCamera, _followSmoothTime);
            transform.position = GetClampedCameraPosition(transform.position);
        }
        
        private Vector3 GetFollowPlayerPosition()
        {
            return _player.position + new Vector3(0f, 1f, -10f);
        }

        private void MoveCamera(Vector2 moveDirection)
        {
            Vector3 move = 20f * Time.deltaTime * (Vector3)moveDirection;
            transform.position += move;
            transform.position = GetClampedCameraPosition(transform.position);
        }

        private Vector3 GetClampedCameraPosition(Vector3 position)
        {
            var clampedPosition = position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _cameraInputConfigurations.LeftThresholdMap, _cameraInputConfigurations.RightThresholdMap);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _cameraInputConfigurations.DownThresholdMap, _cameraInputConfigurations.UpThresholdMap);
            return clampedPosition;
        }

        private bool CheckBallAction(ActionState state)
        {
            if (ActionState.Moving == state) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void HideArrow()
        {
            Vector2 position = transform.position;

            _cameraArrows[0].SetActive(_cameraInputConfigurations.LeftThresholdMap != position.x);  
            _cameraArrows[1].SetActive(_cameraInputConfigurations.RightThresholdMap != position.x); 
            _cameraArrows[2].SetActive(_cameraInputConfigurations.UpThresholdMap != position.y);    
            _cameraArrows[3].SetActive(_cameraInputConfigurations.DownThresholdMap != position.y);  
        }
    }
}
