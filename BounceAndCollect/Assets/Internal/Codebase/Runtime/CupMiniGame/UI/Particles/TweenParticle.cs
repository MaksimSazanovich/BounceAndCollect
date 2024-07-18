using System;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Particles
{
    [DisallowMultipleComponent]
    public abstract class TweenParticle : MonoBehaviour
    {
        public static Action OnFinished;

        public void InvokeOnFinished()
        {
            OnFinished?.Invoke();
        }
    }
}