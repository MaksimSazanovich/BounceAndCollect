using System;
using System.Collections;
using Internal.Codebase.Runtime.Constants;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.CupKeeper
{
    [DisallowMultipleComponent]
    public sealed class CupKeeper : MonoBehaviour
    {
        public Action OnTriggeredBall;
        [SerializeField] private Vector2 point;
        [SerializeField] private Vector2 size;
        [SerializeField] private LayerMask layer;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                //OnTriggeredBall?.Invoke();
                NightPool.Despawn(ballCollision.gameObject);
            }
        }

        private void Update()
        {
            Collider2D[] balls = Physics2D.OverlapBoxAll(point, size, 0 , layer);
            if (balls.Length == 0)
            {
                StartCoroutine(Timer(balls));
            }
        }

        private IEnumerator Timer(Collider2D[] balls)
        {
            yield return new WaitForSeconds(3);
            if (balls.Length == 0)
            {
                Debug.Log("END");
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawCube(point, size);
        }
    }
}