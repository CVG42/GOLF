using UnityEngine;

namespace Golf
{
    public class ForceSliderPosition : MonoBehaviour
    {
        [SerializeField] private Transform _ballTransform;
        [SerializeField] private Vector3 _forceBarOffsetPosition = new Vector3(-1f, 0f, 0f);

        private void OnEnable()
        {            
            transform.position = _ballTransform.position + _forceBarOffsetPosition;
        }
    }
}
