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
        private CupKeeper.CupKeeper cupKeeper;

        [Inject]
        private void Constructor(CupKeeper.CupKeeper cupKeeper)
        {
            this.cupKeeper = cupKeeper;
        }

        private void OnEnable()
        {
            cupKeeper.OnBallsEnded += () => OnEnded?.Invoke();
        }

        private void OnDisable()
        {
            cupKeeper.OnBallsEnded -= () => OnEnded?.Invoke();
        }
    }
}