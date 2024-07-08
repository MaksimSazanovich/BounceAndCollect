using System;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents
{
    [DisallowMultipleComponent]
    public sealed class GameEventsInvoker : MonoBehaviour
    {
        public Action OnStarted;
        public Action OnEnded;
        private CupCatcher.CupCatcher cupCatcher;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher)
        {
            this.cupCatcher = cupCatcher;
        }

        private void OnEnable()
        {
            cupCatcher.OnBallsEnded += () => OnEnded?.Invoke();
        }

        private void OnDisable()
        {
            cupCatcher.OnBallsEnded -= () => OnEnded?.Invoke();
        }
    }
}