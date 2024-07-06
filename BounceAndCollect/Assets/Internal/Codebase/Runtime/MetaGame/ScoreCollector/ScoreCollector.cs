using System;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.MetaGame.ScoreCollector
{
    public sealed class ScoreCollector : MonoBehaviour
    {
        private uint score;
        
        private BallsSpawner ballsSpawner;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(BallsSpawner ballsSpawner, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.ballsSpawner = ballsSpawner;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += AddScore;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= AddScore;
        }

        private void AddScore()
        {
            score += ballsSpawner.SpawnedCount;
        }
    }
}