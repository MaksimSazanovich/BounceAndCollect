using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher
{
    [DisallowMultipleComponent]
    public abstract class Cathcer : MonoBehaviour
    {
        public int CaughtBalls { get; protected set; }
        [SerializeField] protected ParticleSystem particle;
        [SerializeField] protected Text caughtBallsText;
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                AddCaughtBall();
            }
        }

        protected virtual void AddCaughtBall()
        {
            CaughtBalls++;
        }

        protected virtual void ResetCaughtBalls()
        {
            CaughtBalls = 0;
        }
    }
}