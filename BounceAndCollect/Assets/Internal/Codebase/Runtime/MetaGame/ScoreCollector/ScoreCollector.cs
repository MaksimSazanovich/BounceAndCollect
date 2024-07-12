using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.MetaGame.ScoreCollector
{
    public sealed class ScoreCollector : MonoBehaviour
    {
        private int score;
        
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
            gameEventsInvoker.OnEndedPart += AddScore;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEndedPart -= AddScore;
        }

        private void AddScore()
        {
            score += ballsSpawner.SpawnedCount;
        }
    }
}