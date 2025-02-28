using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Golf
{
    public class EndLevelTrigger : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                LevelManager.Source.LoadScene(_sceneName);
            }
        }
    }
}
