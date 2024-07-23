using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather
{
    [DisallowMultipleComponent]
    public sealed class GlassCupBorders : MonoBehaviour
    { 
        private GlassCupCatcher glassCupCatcher;
        [SerializeField] private BoxCollider2D[] boxColliders;
        [SerializeField] private GameObject[] slides;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GlassCupCatcher glassCupCatcher, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.glassCupCatcher = glassCupCatcher;
        }
        private void OnEnable()
        {
            glassCupCatcher.OnOverflowed += Deactivate;
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            glassCupCatcher.OnOverflowed -= Deactivate;
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void Start()
        {
            Restart();
        }

        private void Restart()
        {
            foreach (var boxCollider in boxColliders)
            {
                boxCollider.enabled = true;
            }
            
            foreach (var slide in slides)
            {
                slide.SetActive(true);
            }
        }

        private void Deactivate()
        {
            foreach (var boxCollider in boxColliders)
            {
                boxCollider.enabled = false;
            }

            foreach (var slide in slides)
            {
                slide.SetActive(false);
            }
        }
    }
}