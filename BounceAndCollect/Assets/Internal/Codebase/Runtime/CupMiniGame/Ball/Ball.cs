using System;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.Ball
{
    [DisallowMultipleComponent]
    public sealed class Ball : MonoBehaviour
    {
        [SerializeField] private BallCollision ballCollision;

        public void Reset()
        {
            ballCollision.ResetLockBoosterLineIDs();
        }
    }
}