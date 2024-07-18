using System;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.MetaGame.ScoreCollector
{
    public sealed class ScoreCollector : MonoBehaviour
    {
        private int score;
        public int LevelScore { get; private set; }
        public int BestLevelScore { get; private set; }
        
        private GameEventsInvoker gameEventsInvoker;
        private GlassCupCatcher glassCupCatcher;

        [Inject]
        private void Constructor(GlassCupCatcher glassCupCatcher, GameEventsInvoker gameEventsInvoker)
        {
            this.glassCupCatcher = glassCupCatcher;
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void Start()
        {
            ResetLevelScore();
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
            LevelScore += glassCupCatcher.CaughtBalls;

            if (BestLevelScore < LevelScore)
                BestLevelScore = LevelScore;
            
            score += LevelScore;
        }

        private void ResetLevelScore()
        {
            LevelScore = 0;
        }
    }
}