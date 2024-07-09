using System;
using System.Collections;
using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Factories.BallsFactory;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.MetaGame.GameData;
using Internal.Codebase.Runtime.Shop.Skins;
using Internal.Codebase.Utilities.PositionOffsetCalculator;
using ModestTree;
using NTC.Pool;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.CupMiniGame.BallSpawner
{
    [DisallowMultipleComponent]
    public sealed class BallsSpawner : MonoBehaviour
    {
        [field: SerializeField] public List<Ball.Ball> Balls { get; private set; }
        [field: SerializeField] public int MaxBallsCount { get; private set; }
        
        [SerializeField] private List<Sprite> sprites = new();
        [SerializeField] private BallsSkins currentBallsSkin;
        public int SpawnedCount { get; private set; }
        private int ballsOnStart = 3;
        [SerializeField] private float waitForSecondsRealtime = 0.1f;
        private float timeBetweenSpawnFirstBalls = 0.1f;
        private float spawnOffset = 0.1f;

        private BallsFactory ballsFactory;
        private Cup.Cup cup;
        private CupDropController cupDropController;
        private SkinsResourceProvider skinsResourceProvider;

        public Action<int> OnCreatedBall;
        private CupCatcher.CupCatcher cupCatcher;

        [Inject]
        public void Constructor(BallsFactory ballsFactory, Cup.Cup cup, CupDropController cupDropController,
            SkinsResourceProvider skinsResourceProvider, GameData gameData, CupCatcher.CupCatcher cupCatcher)
        {
            this.cupCatcher = cupCatcher;
            this.skinsResourceProvider = skinsResourceProvider;
            this.cup = cup;
            this.ballsFactory = ballsFactory;
            this.cupDropController = cupDropController;

            ballsOnStart = gameData.BallsOnStart;
        }

        private void OnEnable()
        {
            BallCollision.OnCollidedMultiplierX += CreateSecondBalls;
            cupDropController.OnDropped += Init;
            cupCatcher.OnBallsEnded += SetBallsOnSecondHalf;
        }

        private void OnDisable()
        {
            BallCollision.OnCollidedMultiplierX -= CreateSecondBalls;
            cupDropController.OnDropped -= Init;
            cupCatcher.OnBallsEnded -= SetBallsOnSecondHalf;
        }

        private void Start()
        {
            sprites = skinsResourceProvider.LoadBallsSkinsConfig(currentBallsSkin).Sprites;
            
            if (Balls.IsEmpty())
            {
                CreateFirstBalls();
                DespawnBalls();
            }
        }

        private void SetBallsOnSecondHalf()
        {
            ballsOnStart = cupCatcher.CaughtBalls;
        }

        private void Init()
        {
            StartCoroutine(CreateFirstBallsWithDelay());
        }

        private void CreateFirstBalls()
        {
            for (int i = 0; i < MaxBallsCount; i++)
            {
                Balls.Add(ballsFactory.CreateBall(transform, Vector3.zero, sprites[Random.Range(0, sprites.Count)]));
            }
        }

        private IEnumerator CreateFirstBallsWithDelay()
        {
            for (int i = 1; i <= ballsOnStart; i++)
            {
                SpawnedCount++;
                Balls.Add(ballsFactory.CreateBall(transform, cup.Neck.position,
                    sprites[Random.Range(0, sprites.Count)]));
                OnCreatedBall?.Invoke(ballsOnStart - i);
                yield return new WaitForSeconds(timeBetweenSpawnFirstBalls);
            }
        }

        private void CreateSecondBalls(int count, HashSet<int> lockBoosterLineIDs, Vector3 position)
        {
            StartCoroutine(CreateSecondBalls1(count, lockBoosterLineIDs, position));
        }
        
        private IEnumerator CreateSecondBalls1(int count, HashSet<int> lockBoosterLineIDs, Vector3 position)
        {
            for (int i = 0; i < count; i++)
            {
                SpawnedCount++;
                ballsFactory.CreateBall(transform,
                    PositionOffsetCalculator.CalculateBothAxis(position, spawnOffset), lockBoosterLineIDs,
                    sprites[Random.Range(0, sprites.Count)]);
                
                yield return waitForSecondsRealtime;
            }
        }

        private void DespawnBalls()
        {
            for (int i = 0; i < MaxBallsCount; i++)
            {
                NightPool.Despawn(Balls[i]);
            }
        }
    }
}