using System;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CameraMovement
{
    [DisallowMultipleComponent]
    public sealed class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;
        private GameEventsInvoker gameEventsInvoker;
        private int secondHalfPositionY = -10;
        private GlassCupCatcher glassCupCatcher;
        private float offset = 0.08f;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker, GlassCupCatcher glassCupCatcher)
        {
            this.glassCupCatcher = glassCupCatcher;
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEndedPart += MoveToSecondPart;
            glassCupCatcher.OnOffsetReplaced += MoveToFloor;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEndedPart -= MoveToSecondPart;
            glassCupCatcher.OnOffsetReplaced -= MoveToFloor;
        }

        private void Start()
        {
            transform.position = new(0, 0, transform.position.z);
        }

        private void MoveToSecondPart()
        {
            transform.DOMoveY(secondHalfPositionY, duration).SetEase(ease);
        }

        private void MoveToFloor()
        {
            transform.position = new(transform.position.x, transform.position.y - offset, -10);
        }
    }
}