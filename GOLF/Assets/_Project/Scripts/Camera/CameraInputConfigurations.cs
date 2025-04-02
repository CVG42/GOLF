using UnityEngine;

namespace Golf
{
    [CreateAssetMenu(fileName = "CameraThresholdsData", menuName = "Scriptables/CameraThresholdsData", order = 1)]
    public class CameraInputConfigurations : ScriptableObject
    {
        public float _xOffset_Positive, _xOffset_Negative, _yOffset_Positive, _yOffset_Negative, _zXOffset_Positive, _zXOffset_Negative, _zYOffset_Positive, _zYOffset_Negative;

    }
}
