using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.Ball
{
    [DisallowMultipleComponent]
    public sealed class Ball : MonoBehaviour
    {
        public void Deactivate()
        {
            NightPool.Despawn(gameObject);
        }
    }
}