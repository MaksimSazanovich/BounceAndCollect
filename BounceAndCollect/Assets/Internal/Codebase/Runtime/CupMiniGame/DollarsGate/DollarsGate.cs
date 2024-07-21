using AssetKits.ParticleImage;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.UI.Animations;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.DollarsGate
{
    [DisallowMultipleComponent]
    public sealed class DollarsGate : MonoBehaviour
    {
        [SerializeField] private ParticleImage particle;
        [SerializeField] private UIShakeAnimation text;
        [SerializeField] private BoxCollider2D boxCollider;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }
        private void OnEnable()
        {
            gameEventsInvoker.OnWon += DeactivateCollider;
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnWon -= DeactivateCollider;
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void Start()
        {
            Restart();
        }

        private void Restart()
        {
            boxCollider.enabled = true;
        }

        private void DeactivateCollider()
        {
            boxCollider.enabled = false;
        }

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