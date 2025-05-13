using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class ExitButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
            Debug.Log("Exit game");
        }
    }
}
