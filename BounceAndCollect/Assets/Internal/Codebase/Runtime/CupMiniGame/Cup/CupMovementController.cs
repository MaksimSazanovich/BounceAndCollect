using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Cup
{
    [DisallowMultipleComponent]
    public sealed class CupMovementController : MonoBehaviour
    {
        private Camera camera;
        private Vector3 mousePosition;
        private Vector3 screenBounds;
        private bool canMove;
        [SerializeField] private float leftBoundary;
        [SerializeField] private float rightBoundary;

        public Action OnMouseDown;
        private bool canDrop = true;

        [SerializeField] private float rotateDuration;
        [SerializeField] private Ease rotationEase;

        private CupCatcher.CupCatcher cupCatcher;
        private Vector3 cupCatcherPosition;
        private int replaceOffset = 10;
        [SerializeField] private float replaceDuration;
        [SerializeField] private Ease replaceEase;
        public Action OnReplaced;
        public Action OnStartedReplace;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher)
        {
            this.cupCatcher = cupCatcher;
        }

        private void Start()
        {
            canMove = true;
            camera = Camera.main;
            cupCatcherPosition = cupCatcher.transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, transform.position.y, 0);

                CheckBoundaries();

                if (transform.eulerAngles.z == 0)
                    transform.DORotate(new(0, 0, -90), rotateDuration).SetEase(rotationEase);

                OnMouseDown?.Invoke();
            }
        }

        private void CheckBoundaries()
        {
            float x = Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary);
            transform.position = new Vector3(x, transform.position.y, 0);
        }

        private void OnBecameInvisible()
        {
            float endValue = transform.position.y - replaceOffset;
            
            OnStartedReplace?.Invoke();
            
            canMove = false;
            transform.eulerAngles = Vector3.zero;
            transform.position = cupCatcherPosition;

            transform.DOMoveY(endValue, replaceDuration).SetEase(replaceEase).OnComplete(() => canMove = true);
            OnReplaced?.Invoke();
        }
    }
}