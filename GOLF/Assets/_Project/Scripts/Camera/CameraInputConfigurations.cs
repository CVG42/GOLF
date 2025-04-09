using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "CameraThresholdsData", menuName = "Scriptables/CameraThresholdsData", order = 1)]
    public class CameraInputConfigurations : ScriptableObject
    {
        public float XOffsetPositive;
        public float XOffsetNegative;
        public float YOffsetPositive;
        public float YOffsetNegative;
    }
}
