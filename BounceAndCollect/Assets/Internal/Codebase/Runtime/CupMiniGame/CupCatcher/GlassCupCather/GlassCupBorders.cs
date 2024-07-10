using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather
{
    [DisallowMultipleComponent]
    public sealed class GlassCupBorders : MonoBehaviour
    { 
        private GlassCupCatcher glassCupCatcher;

        [Inject]
        private void Constructor(GlassCupCatcher glassCupCatcher)
        {
            this.glassCupCatcher = glassCupCatcher;
        }
        private void OnEnable()
        {
            glassCupCatcher.OnOverflowed += Deactivate;
        }

        private void OnDisable()
        {
            glassCupCatcher.OnOverflowed -= Deactivate;
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}