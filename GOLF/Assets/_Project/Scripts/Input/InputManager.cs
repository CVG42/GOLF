using System;
using UnityEngine;

namespace Golf
{
    public class InputManager : Singleton<IInputSource>, IInputSource
    {
        [Header("Physics parameters")]
        [SerializeField] private float _maxAngle = 180f;
        [SerializeField] private float _angleChangeSpeed = 50f;

        public Action<Vector2> OnLaunchBall { get; set; } = null;
        public Action OnConfirmButtonPressed { get; set; } = null;
        public float CurrentAngle { get; private set; }
        

        private void Update()
        {
            GetAngle();
            CheckLaunchBall();
            CheckOnConfirmButtonPressed();
        }
        
        private void GetAngle()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                CurrentAngle += _angleChangeSpeed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                CurrentAngle -= _angleChangeSpeed * Time.deltaTime;
            }

            CurrentAngle = Mathf.Clamp(CurrentAngle, 0f, _maxAngle);
        }

        private void CheckLaunchBall()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnLaunchBall?.Invoke(GetCurrentAngleDirection());
            }
        }
        
        public Vector2 GetCurrentAngleDirection()
        {
            var angleInRadians = CurrentAngle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians)).normalized;
        }

        private void CheckOnConfirmButtonPressed()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                OnConfirmButtonPressed?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnLaunchBall = null;
            OnConfirmButtonPressed = null;
        }

    }
}
