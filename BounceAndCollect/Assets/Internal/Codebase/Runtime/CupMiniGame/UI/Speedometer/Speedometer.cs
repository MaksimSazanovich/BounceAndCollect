using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.MetaGame.ScoreCollector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer
{
    [DisallowMultipleComponent]
    public sealed class Speedometer : MonoBehaviour
    {
        private int minValue;
        private int maxValue;

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Transform speedometer;
        [SerializeField] private Transform arrow;
        [SerializeField] private Text percentText;
        private int percent = 30;

        private float animationDuration = 0.4f;
        private Ease ease;
        private Vector3 endScale;
        private GameEventsInvoker gameEventsInvoker;

        public void Constructor(int minValue, int maxValue, float animationDuration, Ease ease, Vector3 endScale)
        {
            this.maxValue = maxValue;
            this.minValue = minValue;

            this.animationDuration = animationDuration;
            this.ease = ease;
            this.endScale = endScale;
        }

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            GlassCupCatcher.Instance.OnAroundCaughtBalls += OnAroundCaughtBalls;
            GameEventsInvoker.Instance.OnEnded += Hide;
        }

        private void OnDisable()
        {
            GlassCupCatcher.Instance.OnAroundCaughtBalls -= OnAroundCaughtBalls;
            GameEventsInvoker.Instance.OnEnded -= Hide;
        }

        public void Show()
        {
            canvasGroup.DOKill();
            gameObject.SetActive(true);

            canvasGroup.alpha = 0;
            speedometer.localScale = Vector3.zero;

            speedometer.DOScale(endScale, animationDuration).SetEase(ease);
            canvasGroup.DOFade(1, animationDuration).SetEase(ease);
        }

        public void Hide()
        {
            canvasGroup.alpha = 0;
        }

        private void OnAroundCaughtBalls()
        {
            if(percent == 100)
                return;
            RotateArrow();
            AddPercent();
        }

        private void RotateArrow()
        {
            arrow.Rotate(Vector3.back);
        }

        private void AddPercent()
        {
            percent++;
            percentText.text = $"{percent}%";
        }
    }
}