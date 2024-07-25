using System;
using DG.Tweening;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.CupMiniGame.Logic.LevelsController;
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
        [SerializeField] private Ease rotationEase;

        private CupCatcher.CupCatcher cupCatcher;
        private Vector3 cupCatcherPosition;
        private int replaceOffset = 10;
        [SerializeField] private float replaceDuration;
        [SerializeField] private Ease replaceEase;
        public event Action OnReplaced;
        public event Action OnStartedReplace;

        private LevelsController levelsController;

        private Vector3 startPosition;
        private GameEventsInvoker gameEventsInvoker;
        [SerializeField] private float mouseOffest;
        private Vector3 targetPosition;

        [Inject]
        private void Constructor(CupCatcher.CupCatcher cupCatcher, LevelsController levelsController,
            GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.levelsController = levelsController;
            this.cupCatcher = cupCatcher;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnRestart += Restart;
            gameEventsInvoker.OnLost += Stop;
            gameEventsInvoker.OnWon += Stop;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnRestart -= Restart;
            gameEventsInvoker.OnLost -= Stop;
            gameEventsInvoker.OnWon -= Stop;
        }

        private void Start()
        {
            camera = Camera.main;
            cupCatcherPosition = cupCatcher.transform.position;
            startPosition = transform.position;
            Restart();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0) && canMove)
            {
                mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
                //transform.position = new Vector3(mousePosition.x - mouseOffest, transform.position.y, 0);
                targetPosition = SetBoundaries(new Vector3(mousePosition.x - mouseOffest, transform.position.y, 0));
                transform.DOMove(targetPosition, 5).SetSpeedBased().SetEase(Ease.OutQuad).OnComplete(() => OnMouseDown?.Invoke());


                

                if (transform.eulerAngles.z == 0)
                {
                    transform.DORotate(new(0, 0, -90), rotateDuration).SetEase(rotationEase);
                }
            }
        }

        private Vector3 SetBoundaries(Vector3 position)
        {
            float x = Mathf.Clamp(position.x, leftBoundary, rightBoundary);
            return new Vector3(x, position.y, 0);
        }

        private void OnBecameInvisible()
        {
            if (levelsController.CurrentPart == LevelParts.First)
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

        private void Restart()
        {
            transform.eulerAngles = Vector3.zero;
            transform.position = startPosition;
            canMove = true;
        }

        private void Stop() => canMove = false;
    }
}