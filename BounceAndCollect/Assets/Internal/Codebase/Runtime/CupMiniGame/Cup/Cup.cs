using System;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
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

        [Inject]
        private void Constructor(BallsSpawner ballsSpawner, CupCatcher.CupCatcher cupCatcher)
        {
            this.cupCatcher = cupCatcher;
            this.ballsSpawner = ballsSpawner;
        }

        private void OnEnable()
        {
            ballsSpawner.OnCreatedBall += ChangeText;
            cupCatcher.OnBallsEnded += () => ChangeText(cupCatcher.CaughtBalls);
        }

        private void OnDisable()
        {
            ballsSpawner.OnCreatedBall -= ChangeText;
            cupCatcher.OnBallsEnded -= () => ChangeText(cupCatcher.CaughtBalls);
        }

        private void ChangeText(int count)
        {
            ballsText.text = count.ToString();
        }
    }
}