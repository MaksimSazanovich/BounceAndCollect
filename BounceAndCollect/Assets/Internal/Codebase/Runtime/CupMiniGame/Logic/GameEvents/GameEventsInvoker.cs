using System;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.UI.Stars;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents
{
    [DisallowMultipleComponent]
    public sealed class GameEventsInvoker : MonoBehaviour
    {
        public Action OnStarted;
        public Action OnEndedPart;
        public Action OnGotThreeStars;
        public Action OnEnded;
        private CupCatcher.CupCatcher cupCatcher;
        private GlassCupCatcher glassCupCatcher;
        private Stars stars;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher, GlassCupCatcher glassCupCatcher, Stars stars)
        {
            this.glassCupCatcher = glassCupCatcher;
            this.cupCatcher = cupCatcher;
            this.stars = stars;
        }

        private void OnEnable()
        {
            cupCatcher.OnBallsEnded += () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded += () => OnEnded?.Invoke();
            stars.OnFilled += () => OnGotThreeStars?.Invoke();
        }

        private void OnDisable()
        {
            cupCatcher.OnBallsEnded -= () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded -= () => OnEnded?.Invoke();
            stars.OnFilled -= () => OnGotThreeStars?.Invoke();
        }
    }
}