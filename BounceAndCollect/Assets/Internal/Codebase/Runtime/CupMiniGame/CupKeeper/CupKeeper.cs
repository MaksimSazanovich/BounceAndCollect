using System;
using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.CupKeeper
{
    [DisallowMultipleComponent]
    public sealed class CupKeeper : MonoBehaviour
    {
        public Action OnBallsEnded;
        [SerializeField] private Vector2 point;
        [SerializeField] private Vector2 size;
        [SerializeField] private LayerMask layer;
        private bool isStart = true;
        private bool isEnd;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                isStart = false;
                NightPool.Despawn(ballCollision.gameObject);
            }
        }

        private void Update()
        {
            if (isStart == false && isEnd == false)
            {
                Collider2D[] balls = Physics2D.OverlapBoxAll(point, size, 0 , layer);
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