using System;
using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.CupMiniGame.Logic.LevelsController;
using Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer;
using NTC.Pool;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather
{
    [DisallowMultipleComponent]
    public sealed class GlassCupCatcher : Catcher
    {
        public event Action OnOverflowed;
        public event Action OnOffsetReplaced;
        public event Action OnAddedCaughtBall;
        public event Action OnAroundCaughtBalls;

        private float offset = 0.052f;

        [SerializeField] private GameObject glassCup;
        [SerializeField] private ParticleSystem explosion;
        private BoxCollider2D boxCollider2D;
        private float boxCollider2DEndOffsetY = 1.1f;
        private Vector2 boxCollider2DStartOffset;
        private LevelsController levelsController;

        public static GlassCupCatcher Instance = null;

        private Vector3 startPosition;
        private GameEventsInvoker gameEventsInvoker;
        private SpeedometerConfig speedometerConfig;

        private float startAreaPointX;
        private float endAreaPointX = -16.3f;

        [Inject]
        private void Constructor(LevelsController levelsController, GameEventsInvoker gameEventsInvoker,
            SpeedometerConfig speedometerConfig)
        {
            this.speedometerConfig = speedometerConfig;
            this.gameEventsInvoker = gameEventsInvoker;
            this.levelsController = levelsController;
        }

        private void OnEnable()
        {
            levelsController.OnChangePart += Reset;
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            levelsController.OnChangePart -= Reset;
            gameEventsInvoker.OnRestart -= Restart;
        }

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            timeBeforeEnd = 3;
            startAreaPointX = point.x;
            boxCollider2D = GetComponent<BoxCollider2D>();
            startPosition = transform.position;
            boxCollider2DStartOffset = boxCollider2D.offset;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            isStart = false;
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                if (CaughtBalls < 200)
                {
                    if (CaughtBalls <= 50)
                    {
                        transform.position = new(transform.position.x, transform.position.y - offset, 0);
                        OnOffsetReplaced?.Invoke();
                    }
                    else
                    {
                        boxCollider2D.offset = new(boxCollider2D.offset.x, boxCollider2DEndOffsetY);
                        boxCollider2D.enabled = false;
                        boxCollider2D.enabled = true;
                        NightPool.Despawn(ballCollision.gameObject);
                    }

                    particle.Play();
                }
                else if (CaughtBalls == 200)
                {
                    OnOverflowed?.Invoke();
                    Explode();
                    point.x = endAreaPointX;
                }

                caughtBallsText.text = $"{CaughtBalls}/50";
            }
        }

        protected override void Update()
        {
            if (levelsController.CurrentPart == LevelParts.Second)
            {
                if (isStart == false && isEnd == false)
                {
                    ballsInFinishArea = Physics2D.OverlapBoxAll(point, size, 0, layer);
                    if (ballsInFinishArea.Length == 0)
                    {
                        //if (timer == null)
                        timer = StartCoroutine(Timer());
                    }
                }

                if (isEnd == false)
                {
                    Debug.Log(CaughtBalls == 0 && cup.Balls == 0 && isEnd == false);
                    if (CaughtBalls == 0 && cup.Balls == 0 && isEnd == false)
                    {
                        timer = StartCoroutine(CupEmptyTimer());
                    }
                }
            }
        }


        protected override void AddCaughtBall()
        {
            base.AddCaughtBall();
            OnAddedCaughtBall?.Invoke();

            if (CaughtBalls % speedometerConfig.Step == 0)
                OnAroundCaughtBalls?.Invoke();
        }

        private void Explode()
        {
            explosion.Play();
            glassCup.SetActive(false);
        }

        private void Reset(LevelParts obj)
        {
            isStart = true;
            isEnd = false;
            if (timer != null)
                StopCoroutine(timer);
        }

        private void Restart()
        {
            CaughtBalls = 0;
            transform.position = startPosition;
            Reset(LevelParts.First);
            boxCollider2D.offset = boxCollider2DStartOffset;
            glassCup.SetActive(true);
            caughtBallsText.text = $"{CaughtBalls}/50";
            point.x = startAreaPointX;
        }
    }
}