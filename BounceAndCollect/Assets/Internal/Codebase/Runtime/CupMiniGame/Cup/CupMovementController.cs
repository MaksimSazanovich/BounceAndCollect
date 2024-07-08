using System;
using DG.Tweening;
using UnityEngine;
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
        [SerializeField] private Ease ease;
        private CupCatcher.CupCatcher cupCatcher;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher)
        {
            this.cupCatcher = cupCatcher;
        }
        private void Start()
        {
            canMove = true;
            camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x, transform.position.y, 0);

                CheckBoundaries();

                if (transform.eulerAngles.z == 0)
                    transform.DORotate(new(0, 0, -90), rotateDuration).SetEase(ease);

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
            canMove = false;
            transform.eulerAngles = Vector3.zero;
            transform.position = cupCatcher.transform.position;
        }
    }
}