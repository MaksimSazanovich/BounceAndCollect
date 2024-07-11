using System;
using AssetKits.ParticleImage;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.UI.Animations;
using NaughtyAttributes;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.DollarsGate
{
    [DisallowMultipleComponent]
    public sealed class DollarsGate : MonoBehaviour
    {
        [SerializeField] private ParticleImage particle;
        [SerializeField] private UIShakeAnimation text;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                NightPool.Despawn(ballCollision.gameObject);
                particle.Play();
                text.Play();
            }
        }
    }
}