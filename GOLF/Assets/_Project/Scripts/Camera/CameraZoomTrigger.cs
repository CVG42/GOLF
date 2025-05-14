using DG.Tweening;
using UnityEngine;

namespace Golf
{
    public class CameraZoomTrigger : MonoBehaviour
    {
        [SerializeField] private float _zoomSize = 3.5f;
        [SerializeField] private float _zoomingDuration = 1f;
        [SerializeField] private float originalSize = 8f;

        private bool _isZooming = false;
        private Tween _zoomCameraTween;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !_isZooming)
            {
                _isZooming = true;

                var camera = Camera.main;
                if (camera != null) 
                {
                    _zoomCameraTween?.Kill();
                    _zoomCameraTween = camera.DOOrthoSize(_zoomSize, _zoomingDuration);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var camera = Camera.main;

                if (camera != null)
                {
                    _zoomCameraTween?.Kill();
                    _zoomCameraTween = camera.DOOrthoSize(originalSize, _zoomingDuration);
                }

                _isZooming = false;
            }
        }

        private void OnDestroy()
        {
            _zoomCameraTween?.Kill();
        }
    }
}
