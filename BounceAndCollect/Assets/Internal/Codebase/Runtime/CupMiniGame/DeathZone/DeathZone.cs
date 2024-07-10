using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.DeathZone
{
    [DisallowMultipleComponent]
    public sealed class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out BallCollision ballCollision))
                NightPool.Despawn(ballCollision.gameObject);  
        }
    }
}