using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;
using Zenject;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;

namespace Internal.Codebase.Infrastructure.Factories.BallsFactory
{
    public sealed class BallsFactory
    {
        private ResourceProvider resourceProvider;

        [Inject]
        private void Constructor(ResourceProvider resourceProvider)
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