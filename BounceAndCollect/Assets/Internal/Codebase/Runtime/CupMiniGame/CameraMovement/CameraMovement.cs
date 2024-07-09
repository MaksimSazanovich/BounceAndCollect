using DG.Tweening;
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
        private int endValue = -10;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += MoveToSecondHalf;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= MoveToSecondHalf;
        }

        private void MoveToSecondHalf()
        {
            transform.DOMoveY(endValue, duration).SetEase(ease);
        }
    }
}