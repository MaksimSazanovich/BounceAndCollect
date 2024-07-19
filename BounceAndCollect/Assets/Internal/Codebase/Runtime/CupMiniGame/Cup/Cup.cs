using System;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Cup
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CupMovementController))]
    public sealed class Cup : MonoBehaviour
    {
        private BallsSpawner ballsSpawner;
        [field: SerializeField] public Transform Neck { get; private set; }
        [SerializeField] private Text ballsText;
        private CupCatcher.CupCatcher cupCatcher;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(BallsSpawner ballsSpawner, CupCatcher.CupCatcher cupCatcher,
            GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.cupCatcher = cupCatcher;
            this.ballsSpawner = ballsSpawner;
        }

        private void OnEnable()
        {
            ballsSpawner.OnCreatedBall += ChangeText;
            cupCatcher.OnBallsEnded += () => ChangeText(cupCatcher.CaughtBalls);
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            ballsSpawner.OnCreatedBall -= ChangeText;
            cupCatcher.OnBallsEnded -= () => ChangeText(cupCatcher.CaughtBalls);
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void ChangeText(int count)
        {
            ballsText.text = count.ToString();
        }

        private void Restart()
        {
            ChangeText(3);
        }
    }
}