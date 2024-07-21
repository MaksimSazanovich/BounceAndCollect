using System;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.UI.Stars;
using Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel.Restart;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents
{
    [DisallowMultipleComponent]
    public sealed class GameEventsInvoker : MonoBehaviour
    {
        public event Action OnStarted;
        public event Action OnEndedPart;
        public event Action OnGotThreeStars;
        public event Action OnWon;

        public event Action OnLost;
        public event Action OnRestart;

        private CupCatcher.CupCatcher cupCatcher;
        private GlassCupCatcher glassCupCatcher;
        private Stars stars;

        public static GameEventsInvoker Instance;
        private RestartButton[] restartButtons;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher, GlassCupCatcher glassCupCatcher, Stars stars,
            RestartButton[] restartButton)
        {
            this.restartButtons = restartButton;
            this.glassCupCatcher = glassCupCatcher;
            this.cupCatcher = cupCatcher;
            this.stars = stars;
        }

        private void OnEnable()
        {
            cupCatcher.OnBallsEnded += () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded += InvokeWinOrLose;
            stars.OnFilled += () => OnGotThreeStars?.Invoke();
            foreach (var restartButton in restartButtons)
            {
                restartButton.OnClicked += () => OnRestart?.Invoke();
            }
            
        }

        private void OnDisable()
        {
            cupCatcher.OnBallsEnded -= () => OnEndedPart?.Invoke();
            glassCupCatcher.OnBallsEnded -= InvokeWinOrLose;
            stars.OnFilled -= () => OnGotThreeStars?.Invoke();
            foreach (var restartButton in restartButtons)
            {
                restartButton.OnClicked -= () => OnRestart?.Invoke();
            }
        }

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        private void InvokeWinOrLose()
        {
            if(glassCupCatcher.CaughtBalls >= 50)
                OnWon?.Invoke();
            else OnLost?.Invoke();
        }
    }
}