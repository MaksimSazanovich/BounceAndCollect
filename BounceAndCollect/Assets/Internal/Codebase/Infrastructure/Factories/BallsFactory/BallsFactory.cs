using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factories.BallsFactory
{
    class BallsFactory : IBallsFactory
    {
        private IResourceProvider resourceProvider;

        [Inject]
        private void Constructor(IResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
        }
        public Ball CreateBall(Transform at, Vector3 position, HashSet<int> lockBoosterLineIDs, Sprite sprite)
        {
            var config = resourceProvider.LoadBallConfig();
            var ball = NightPool.Spawn(config.Ball, position, Quaternion.identity, at);
            ball.GetComponent<BallCollision>().Constructor(lockBoosterLineIDs);
            ball.GetComponent<SpriteRenderer>().sprite = sprite;
            return ball;
        }

        public Ball CreateBall(Transform at, Vector3 postion, Sprite sprite)
        {
            var config = resourceProvider.LoadBallConfig();
            var ball = NightPool.Spawn(config.Ball, postion, Quaternion.identity, at);
            ball.GetComponent<SpriteRenderer>().sprite = sprite;
            return ball;
        }
    }
}