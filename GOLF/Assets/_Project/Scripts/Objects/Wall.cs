using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Golf
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private Collider2D _wallCollider;
        [SerializeField] private int _wallHardness;

        [SerializeField] private float _slowTimeScale = 0.1f;
        [SerializeField] private float _slowdownDuration = 0.4f;

        [SerializeField] private float _cameraShakeDuration = 1f;
        [SerializeField] private AnimationCurve _curve;

        [SerializeField] private Animator[] _wallAnimators;
        [SerializeField] private string _animatorParameter;

        private Vector3 _originalPosition;
        private bool _isShaking = false;

        private void Start()
        {
            PowerUpSystem.Source.OnPowerUpSelected += SetColliderType;
        }

        private void OnDestroy()
        {
            PowerUpSystem.Source.OnPowerUpSelected -= SetColliderType;
        }

        private void SetColliderType(PowerUpData powerUpData)
        {
            _wallCollider.isTrigger = powerUpData.Hardness > _wallHardness;
        }

        private async void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || _isShaking) return;

            _isShaking = true;

            float originalTimeScale = Time.timeScale;
            float originalFixedDeltaTime = Time.fixedDeltaTime;

            Time.timeScale = _slowTimeScale;
            Time.fixedDeltaTime = originalFixedDeltaTime * _slowTimeScale;

            PlayAnimation();
            var shakeTask = ShakeCamera();

            await UniTask.Delay(TimeSpan.FromSeconds(_slowdownDuration), DelayType.UnscaledDeltaTime);

            Time.timeScale = originalTimeScale;
            Time.fixedDeltaTime = originalFixedDeltaTime;

            await shakeTask;

            await UniTask.Delay(TimeSpan.FromSeconds(1f), DelayType.UnscaledDeltaTime);

            DeactivateObjects(false);
            _isShaking = false;
        }

        private async UniTask ShakeCamera()
        {
            Camera camera = Camera.main;
            
            if (camera == null) return;

            Transform cameraTransform = camera.transform;
            _originalPosition = cameraTransform.position;

            float elapsed = 0f;

            while (elapsed < _cameraShakeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float strength = _curve.Evaluate(elapsed / _cameraShakeDuration);
                Vector2 offset = UnityEngine.Random.insideUnitCircle * strength;
                cameraTransform.position = _originalPosition + new Vector3(offset.x, offset.y, 0f);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            cameraTransform.position = _originalPosition;
        }

        private void PlayAnimation()
        {
            for (int i = 0; i < _wallAnimators.Length; i++)
            {
                int animationIndex = (i == 0) ? 1 : 2;
                _wallAnimators[i].SetInteger(_animatorParameter, animationIndex);
            }
        }

        private void DeactivateObjects(bool state)
        {
            gameObject.SetActive(state);

            for (int i = 0; i < _wallAnimators.Length; i++)
            {
                _wallAnimators[i].gameObject.SetActive(state);
            }
        }
    }
}
