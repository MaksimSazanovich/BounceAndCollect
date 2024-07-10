using System;
using System.Collections;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher
{
    [DisallowMultipleComponent]
    public sealed class CupCatcher : Cathcer
    {
        public Action OnBallsEnded;
        [SerializeField] private Vector2 point;
        [SerializeField] private Vector2 size;
        [SerializeField] private LayerMask layer;
        private bool isStart = true;
        private bool isEnd;

        private float shakeOffset = 0.1f;
        private float shakePositionY;
        private float startPositionY;
        private float shakeDuration = 0.1f;
        [SerializeField] private Ease ease;
        
        private CupMovementController cupMovementController;
        private BallsSpawner ballsSpawner;
        
        [Inject]
        private void Constructor(CupMovementController cupMovementController, BallsSpawner ballsSpawner)
        {
            this.ballsSpawner = ballsSpawner;
            this.cupMovementController = cupMovementController;
        }

        private void OnEnable()
        {
            cupMovementController.OnStartedReplace += Deactivate;
        }

        private void OnDisable()
        {
            cupMovementController.OnStartedReplace -= Deactivate;
        }

        private void Start()
        {
            Reset();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                NightPool.Despawn(ballCollision.gameObject);
                isStart = false;
                particle.Play();
                Shake();
            }
        }

        protected override void AddCaughtBall()
        {
            base.AddCaughtBall();
            caughtBallsText.text = CaughtBalls.ToString();
        }

        private void Shake()
        {
            transform.DOMoveY(shakePositionY, shakeDuration).SetEase(ease)
                .OnComplete(() => transform.DOMoveY(startPositionY, shakeDuration).SetEase(ease));
        }

        private void Update()
        {
            if (isStart == false && isEnd == false)
            {
                Collider2D[] balls = Physics2D.OverlapBoxAll(point, size, 0, layer);
                if (balls.Length == 0)
                {
                    StartCoroutine(Timer(balls));
                }
            }
        }

        private IEnumerator Timer(Collider2D[] balls)
        {
            yield return new WaitForSeconds(3);
            if (balls.Length == 0 && isEnd == false)
            {
                isEnd = true;
                caughtBallsText.text = ballsSpawner.SpawnedCount.ToString();
                OnBallsEnded?.Invoke();
            }
        }

        private void Deactivate()
        {
            Reset();
            gameObject.SetActive(false);
        }

        private void Reset()
        {
            startPositionY = transform.position.y;
            shakePositionY = transform.position.y - shakeOffset;
            
            ResetCaughtBalls();
        }


        protected override void ResetCaughtBalls()
        {
            base.ResetCaughtBalls();
            caughtBallsText.text = CaughtBalls.ToString();

        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(point, size);
        }
    }
}