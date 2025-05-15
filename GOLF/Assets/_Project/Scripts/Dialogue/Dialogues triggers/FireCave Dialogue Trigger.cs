using DG.Tweening;
using UnityEngine;

namespace Golf
{
    public class FireCaveDialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private GameObject _boxingCat, _powerupUI;
        [SerializeField] private CanvasGroup _canvasBlink;

        private Sequence _firePowerupSequence;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var _rbCollider = collision.GetComponent<Rigidbody2D>();
            _rbCollider.angularVelocity = 0f;
            _rbCollider.velocity = Vector3.zero;
            TriggerDialogue();
            gameObject.SetActive(false);
        }

        private void TriggerDialogue()
        {
            GameStateManager.Source.ChangeState(GameState.OnDialogue);
            DialogueManager.Source.StartCinematicDialogue(_dialogue, PowerUpSequence);
        }

        private void PowerUpSequence()
        {
            _firePowerupSequence.Kill();
            _firePowerupSequence = DOTween.Sequence()
                .Append(_canvasBlink.DOFade(1, 2))
                .AppendCallback(() => _boxingCat.SetActive(false))
                .Append(_canvasBlink.DOFade(0, 2))
                .AppendCallback(() => _powerupUI.SetActive(true));
        }

    }
}
