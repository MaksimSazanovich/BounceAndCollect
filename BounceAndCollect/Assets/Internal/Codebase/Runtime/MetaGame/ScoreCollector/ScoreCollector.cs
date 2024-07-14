using System;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.MetaGame.ScoreCollector
{
    public sealed class ScoreCollector : MonoBehaviour
    {
        private int score;
        private int levelScore;
        
        private BallsSpawner ballsSpawner;
        private GameEventsInvoker gameEventsInvoker;
        
        [Inject]
        private void Constructor(BallsSpawner ballsSpawner, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.ballsSpawner = ballsSpawner;
        }

        private void Start()
        {
     
            
            levelScore = 0;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEndedPart += AddScore;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEndedPart -= AddScore;
        }

        private void AddScore()
        {
            levelScore += ballsSpawner.SpawnedCount;
            
            
            
            score += levelScore;
            levelScore = 0;
        }
    }
}