using System;
using System.Collections.Generic;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.PusherUp;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Subtractor;
using NaughtyAttributes;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.Ball
{
    [RequireComponent(typeof(CircleCollider2D))]
    [DisallowMultipleComponent]
    public sealed class BallCollision : MonoBehaviour
    {
        private CircleCollider2D collider2D;
        public HashSet<int> LockBoosterLineIDs { get; private set; } = new();
        public static event Action<int, HashSet<int>, Vector3> OnCollidedMultiplierX;

        public static event Action OnTriggeredCupKeeper;

        private void Start()
        {
            collider2D = GetComponent<CircleCollider2D>();
        }

        public void Constructor(HashSet<int> lockBoosterLineIDs)
        {
            LockBoosterLineIDs = new HashSet<int>(lockBoosterLineIDs);
        }

        public void ResetLockBoosterLineIDs()
        {
            LockBoosterLineIDs.Clear();
        }

        public void Lock(BoosterLine boosterLine)
        {
            boosterLine.GetComponent<BoosterLineCollision>().TriggerEnter2D(this);
            switch (boosterLine)
            {
                case MultiplierX:
                {
                    var multiplierX = boosterLine.GetComponent<MultiplierX>();
                    LockBoosterLineIDs.Add(multiplierX.ID);
                    OnCollidedMultiplierX?.Invoke(multiplierX.Value - 1, LockBoosterLineIDs,
                        transform.position);
                }
                    break;
                case PusherUp:
                {
                    var pusherUp = boosterLine.GetComponent<PusherUp>();
                    LockBoosterLineIDs.Clear();
                    LockBoosterLineIDs.Add(pusherUp.ID);
                }
                    break;
                case Subtractor:
                    NightPool.Despawn(gameObject);
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CupCatcher.CupCatcher cupKeeper))
                OnTriggeredCupKeeper?.Invoke();
        }

        [Button]
        private void DebugHashSet()
        {
            Debug.Log("Start");
            foreach (var lockBoosterLineID in LockBoosterLineIDs)
            {
                Debug.Log(lockBoosterLineID);
            }

            Debug.Log("Finish");
        }
    }
}