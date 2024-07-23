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
    public abstract class Catcher : MonoBehaviour
    {
        public int CaughtBalls { get; protected set; }
        [SerializeField] protected ParticleSystem particle;
        [SerializeField] protected Text caughtBallsText;

        protected bool isStart = true;
        protected bool isEnd;

        protected BallsSpawner ballsSpawner;

        public event Action OnBallsEnded;
        [SerializeField] protected Vector2 point;
        [SerializeField] protected Vector2 size;
        [SerializeField] protected LayerMask layer;
        protected int timeBeforeEnd;
        protected Collider2D[] ballsInFinishArea;
        protected Cup.Cup cup;

        protected Coroutine timer;

        [Inject]
        protected virtual void Constructor(BallsSpawner ballsSpawner, Cup.Cup cup)
        {
            this.ballsSpawner = ballsSpawner;
            this.cup = cup;
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
                    //if (timer == null)
                        timer = StartCoroutine(Timer());
                }
            }

           
            /*
            if (CaughtBalls == 0 && cup.Balls == 0)
            {
                if (timer == null)
                    timer = StartCoroutine(Timer());
            }
            else timer = null;*/
        }

        protected IEnumerator Timer()
        {
            yield return new WaitForSeconds(timeBeforeEnd);
            if (ballsInFinishArea.Length == 0 && isEnd == false && isStart == false)
            {
                isEnd = true;
                OnBallsEnded?.Invoke();
                Debug.Log(nameof(OnBallsEnded));
            }
        }
        
        protected IEnumerator CupEmptyTimer()
        {
            yield return new WaitForSeconds(timeBeforeEnd);
            if (CaughtBalls == 0 && cup.Balls == 0 && isEnd == false)
            {
                isEnd = true;
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