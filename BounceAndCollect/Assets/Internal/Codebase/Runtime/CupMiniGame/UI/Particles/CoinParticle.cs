using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Particles
{
    public sealed class CoinParticle : MonoBehaviour
    {
        public static Action OnFinished;

        public void InvokeOnFinished()
        {
            OnFinished?.Invoke();
        }
    }
}