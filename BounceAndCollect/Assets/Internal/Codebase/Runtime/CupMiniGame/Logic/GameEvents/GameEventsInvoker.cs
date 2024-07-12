using System;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents
{
    [DisallowMultipleComponent]
    public sealed class GameEventsInvoker : MonoBehaviour
    {
        public Action OnStarted;
        public Action OnEndedPart;
        public Action OnEnded;
        private CupCatcher.CupCatcher cupCatcher;
        private GlassCupCatcher glassCupCatcher;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher, GlassCupCatcher glassCupCatcher)
        {
            this.glassCupCatcher = glassCupCatcher;
            this.cupCatcher = cupCatcher;
        }

        private void OnEnable()
        {
            cupCatcher.OnBallsEnded += () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded += () => OnEnded?.Invoke();
        }

        private void OnDisable()
        {
            cupCatcher.OnBallsEnded -= () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded -= () => OnEnded?.Invoke();
        }
    }
}