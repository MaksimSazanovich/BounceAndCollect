using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Stars
{
    public sealed class StarsCounterView : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }
        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnWon += Show;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnWon -= Show;
        }

        private void Show()
        {
            canvasGroup.DOFade(1, 0.5f);
        }
    }
}