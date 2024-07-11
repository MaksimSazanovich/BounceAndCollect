using System;
using System.Collections;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher
{
    [DisallowMultipleComponent]
    public sealed class CupCatcher : Cathcer
    {

        

        private float shakeOffset = 0.1f;
        private float shakePositionY;
        private float startPositionY;
        private float shakeDuration = 0.1f;
        [SerializeField] private Ease ease;
        
        private CupMovementController cupMovementController;


        [Inject]
        protected override void Constructor(BallsSpawner ballsSpawner)
        {
            base.Constructor(ballsSpawner);
        }

        [Inject]
        void Constructor(CupMovementController cupMovementController)
        {
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
            timeBeforeEnd = 3;
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
    }
}