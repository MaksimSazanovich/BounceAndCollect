using System;
using System.Collections;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel
{
    [DisallowMultipleComponent]
    public sealed class WinPanel : MonoBehaviour
    {
        private GameEventsInvoker gameEventsInvoker;
        [SerializeField] private CanvasGroup winPanel;
        [SerializeField] private CanvasGroup restartButton;

        public event Action OnShowed;
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
            gameEventsInvoker.OnWon += () => StartCoroutine(Activate());
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnWon -= () => StartCoroutine(Activate());
            gameEventsInvoker.OnRestart -= Restart;
        }

        private IEnumerator Activate()
        {
            yield return new WaitForSeconds(1);
            winPanel.alpha = 1;
            OnShowed?.Invoke();
            yield return new WaitForSeconds(2);
            restartButton.DOFade(1,0.5f);
        }

        private void Restart()
        {
            restartButton.alpha = 0;
            winPanel.alpha = 0;
        }
    }
}