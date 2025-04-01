using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Golf
{
    public class ResolutionSettingsMenu : MonoBehaviour
    {
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private Dropdown _resolutionDropdown;

        private ISaveSource saveSystem;

        private readonly List<Vector2Int> resolutions = new List<Vector2Int>
        {
            new Vector2Int(1280, 720),
            new Vector2Int(1920, 1080),
            new Vector2Int(2560, 1440),
            new Vector2Int(3840, 2160)
        };

        private void Awake()
        {
            saveSystem = SaveSystem.Source;
        }

        private void Start()
        {
            if (saveSystem != null)
            {
                _fullScreenToggle.isOn = saveSystem.GetFullScreenMode();
                _fullScreenToggle.onValueChanged.AddListener(SetFullScreenMode);

                SetupResolutionDropdown();
            }
        }

        private void SetupResolutionDropdown()
        {
            _resolutionDropdown.ClearOptions();
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

            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.value = currentIndex;
            _resolutionDropdown.onValueChanged.AddListener(SetResolution);
            _resolutionDropdown.interactable = !saveSystem.GetFullScreenMode();
        }

        public void SetFullScreenMode(bool isFullScreen)
        {
            saveSystem.SetFullScreenMode(isFullScreen);
            _resolutionDropdown.interactable = !isFullScreen;
        }

        public void SetResolution(int index)
        {
            Vector2Int selectedResolution = resolutions[index];
            saveSystem.SetResolution(selectedResolution.x, selectedResolution.y);
        }
    }
}
