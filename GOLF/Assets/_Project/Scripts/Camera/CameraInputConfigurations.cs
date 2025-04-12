using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "CameraThresholdsData", menuName = "Scriptables/CameraThresholdsData", order = 1)]
    public class CameraInputConfigurations : ScriptableObject
    {
        public float RightThresholdMap;
        public float LeftThresholdMap;
        public float UpThresholdMap;
        public float DownThresholdMap;
    }
}
