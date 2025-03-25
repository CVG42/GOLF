using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Golf
{
    public class SettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle fullScreenToggle;
        [SerializeField] private Dropdown resolutionDropdown;
        private SaveSystem saveSystem;

        private readonly List<Vector2Int> resolutions = new List<Vector2Int>
        {
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440),
            new Vector2Int(3840, 2160)
        };

        private void Start()
        {
            if (saveSystem != null)
            {
                fullScreenToggle.isOn = saveSystem.GetFullScreenMode();
                fullScreenToggle.onValueChanged.AddListener(SetFullScreenMode);

                SetupResolutionDropdown();
            }
        }

        private void SetupResolutionDropdown()
        {
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();

            Vector2Int currentResolution = saveSystem.GetResolution();
            int currentIndex = 0;

            for (int i = 0; i < resolutions.Count; i++)
            {
                string option = resolutions[i].x + " x " + resolutions[i].y;
                options.Add(option);

                if (resolutions[i] == currentResolution)
                    currentIndex = i;
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentIndex;
            resolutionDropdown.onValueChanged.AddListener(SetResolution);
            resolutionDropdown.interactable = !saveSystem.GetFullScreenMode();
        }

        public void SetFullScreenMode(bool isFullScreen)
        {
            saveSystem.SetFullScreenMode(isFullScreen);
            resolutionDropdown.interactable = !isFullScreen;
        }

        public void SetResolution(int index)
        {
            Vector2Int selectedRes = resolutions[index];
            saveSystem.SetResolution(selectedRes.x, selectedRes.y);
        }
    }
}
