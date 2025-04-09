using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Golf
{
    public class CameraInputManager : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraInputConfigurations _cameraInputConfigurations;
        [SerializeField] private float _followSmoothTime = 0.1f;

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
            }
            else
            {
                InputManager.Source.IsLocking = false;
            }
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
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, _cameraInputConfigurations.XOffsetNegative, _cameraInputConfigurations.XOffsetPositive);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, _cameraInputConfigurations.YOffsetNegative, _cameraInputConfigurations.YOffsetPositive);
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
    }
}
