using System;
using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher
{
    [DisallowMultipleComponent]
    public abstract class Cathcer : MonoBehaviour
    {
        public int CaughtBalls { get; protected set; }
        [SerializeField] protected ParticleSystem particle;
        [SerializeField] protected Text caughtBallsText;
        
        protected bool isStart = true;
        protected bool isEnd;
        
        private BallsSpawner ballsSpawner;
        
        public Action OnBallsEnded;
        [SerializeField] protected Vector2 point;
        [SerializeField] protected Vector2 size;
        [SerializeField] protected LayerMask layer;
        protected int timeBeforeEnd;
        private Collider2D[] ballsInFinishArea;

        [Inject]
        protected virtual void Constructor(BallsSpawner ballsSpawner)
        {
            this.ballsSpawner = ballsSpawner;
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                AddCaughtBall();
            }
        }
        
        protected virtual void Update()
        {
            if (isStart == false && isEnd == false)
            {
                ballsInFinishArea = Physics2D.OverlapBoxAll(point, size, 0, layer);
                if (ballsInFinishArea.Length == 0)
                {
                    StartCoroutine(Timer());
                }
            }
        }

        protected virtual IEnumerator Timer()
        {
            yield return new WaitForSeconds(timeBeforeEnd);
            if (ballsInFinishArea.Length == 0 && isEnd == false)
            {
                isEnd = true;
                //caughtBallsText.text = ballsSpawner.SpawnedCount.ToString();
                OnBallsEnded?.Invoke();
                Debug.Log(nameof(OnBallsEnded));
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
        
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(point, size);
        }
    }
}