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
    public sealed class CupCatcher : MonoBehaviour
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

        [SerializeField] private Text caughtBallsText;
        public int CaughtBalls { get; private set; }

        private Vector3 endPosition = new(0, -14.2f);
        private CupMovementController cupMovementController;
        private BallsSpawner ballsSpawner;

        [SerializeField] private ParticleSystem particle;

        [Inject]
        private void Constructor(CupMovementController cupMovementController, BallsSpawner ballsSpawner)
        {
            this.ballsSpawner = ballsSpawner;
            this.cupMovementController = cupMovementController;
        }

        private void OnEnable()
        {
            cupMovementController.OnStartedReplace += Replace;
        }

        private void OnDisable()
        {
            cupMovementController.OnStartedReplace -= Replace;
        }

        private void Start()
        {
            Reset();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                AddCaughtBall();
                
                particle.Play();
                
                isStart = false;
                NightPool.Despawn(ballCollision.gameObject);

                Shake();
            }
        }

        private void AddCaughtBall()
        {
            CaughtBalls++;
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

        private void Replace()
        {
            transform.position = endPosition;
            Reset();
        }

        private void Reset()
        {
            startPositionY = transform.position.y;
            shakePositionY = transform.position.y - shakeOffset;
            
            ResetCaughtBalls();
        }

        private void ResetCaughtBalls()
        {
            CaughtBalls = 0;
            caughtBallsText.text = CaughtBalls.ToString();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(point, size);
        }
    }
}