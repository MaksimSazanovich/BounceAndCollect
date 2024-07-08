using System;
using System.Collections;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher
{
    [DisallowMultipleComponent]
    internal sealed class CupCatcher : MonoBehaviour
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
        private int caughtBalls;

        private void Start()
        {
            startPositionY = transform.position.y;
            shakePositionY = transform.position.y - shakeOffset;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                AddCaughtBall();
                
                isStart = false;
                NightPool.Despawn(ballCollision.gameObject);

                Shake();
            }
        }

        private void AddCaughtBall()
        {
            caughtBalls++;
            caughtBallsText.text = caughtBalls.ToString();
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
                OnBallsEnded?.Invoke();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(point, size);
        }
    }
}