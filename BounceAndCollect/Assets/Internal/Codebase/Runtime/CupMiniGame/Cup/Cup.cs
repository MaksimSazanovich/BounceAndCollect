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

        [Inject]
        private void Constructor(BallsSpawner ballsSpawner)
        {
            this.ballsSpawner = ballsSpawner;
        }

        private void OnEnable()
        {
            ballsSpawner.OnCreatedBall += ChangeText;
        }

        private void OnDisable()
        {
            ballsSpawner.OnCreatedBall -= ChangeText;
        }

        private void ChangeText(int count)
        {
            ballsText.text = count.ToString();
        }
    }
}