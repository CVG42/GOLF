using TMPro;
using UnityEngine;

namespace Golf
{
    public class PlayButton : MonoBehaviour
    {
        [SerializeField] private TMP_Text _playButtonText;

        private void Start()
        {
            ChangePlayButtonText();
        }

        private void Update()
        {
            ChangePlayButtonText();
        }

        private void ChangePlayButtonText()
        {
            if (SaveSystem.Source.DoesAnyFileExist())
            {
                _playButtonText.text = "Continue".Localize();
            }
            else
            {
                _playButtonText.text = "New Game".Localize();
            }
        }
    }
}
