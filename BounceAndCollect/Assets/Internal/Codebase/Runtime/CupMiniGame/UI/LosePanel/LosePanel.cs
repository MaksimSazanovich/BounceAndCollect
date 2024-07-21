using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.LosePanel
{
    [DisallowMultipleComponent]
    public sealed class LosePanel : MonoBehaviour
    {
        private GameEventsInvoker gameEventsInvoker;
        private float duration = 0.3f;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void Start()
        {
            Restart();
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnLost += Show;
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnLost -= Show;
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void Show()
        {
            transform.DOScale(Vector3.one, duration).SetEase(Ease.InOutBounce);
        }

        private void Restart()
        {
            transform.localScale = Vector3.zero;
        }
    }
}