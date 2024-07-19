using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.ConfettiCannons
{
    [DisallowMultipleComponent]
    public sealed class ConfettiCannons : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] cannons;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += Shoot;
            gameEventsInvoker.OnRestart += Stop;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= Shoot;
            gameEventsInvoker.OnRestart -= Stop;
        }

        private void Shoot()
        {
            foreach (var cannon in cannons)
            {
                cannon.Play();
            }
        }

        private void Stop()
        {
            foreach (var cannon in cannons)
            {
                cannon.Stop();
            }
        }
    }
}