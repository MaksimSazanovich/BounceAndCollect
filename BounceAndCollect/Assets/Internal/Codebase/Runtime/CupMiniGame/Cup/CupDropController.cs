using System;
using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Cup
{
    [DisallowMultipleComponent]
    public sealed class CupDropController : MonoBehaviour
    {
        public Action OnDropped;
        
        private CupMovementController movementController;
        private bool canDrop = true;
        private float timeBeforeDrop = 0.3f;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }
        private void Awake()
        {
            movementController = GetComponent<CupMovementController>();
        }

        private void OnEnable()
        {
            movementController.OnMouseDown += Push;
            movementController.OnReplaced += Restart;
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            movementController.OnMouseDown -= Push;
            movementController.OnReplaced -= Restart;
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void Push()
        {
            if (canDrop)
                StartCoroutine(DropTimer());
        }

        private IEnumerator DropTimer()
        {
            canDrop = false;
            yield return new WaitForSeconds(timeBeforeDrop);
            OnDropped?.Invoke();
        }

        private void Restart()
        {
            canDrop = true;
        }
    }
}