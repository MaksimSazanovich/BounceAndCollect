using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.CupMiniGame.Logic.LevelsController;
using Internal.Codebase.Runtime.CupMiniGame.UI.Stars;
using Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel.Restart;
using Internal.Codebase.Runtime.MetaGame.ScoreCollector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms.Impl;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class CupMiniGameInstaller : MonoInstaller
    {
        [SerializeField] private BallsSpawner ballsSpawner;
        [SerializeField] private Cup cup;
        [SerializeField] private CupDropController cupDropController;
        [SerializeField] private CupCatcher cupCatcher;
        [SerializeField] private GlassCupCatcher glassCupCatcher;
        [SerializeField] private GameEventsInvoker gameEventsInvoker;
        [SerializeField] private CupMovementController cupMovementController;
        [SerializeField] private LevelsController levelsController;
        [SerializeField] private Stars stars;
        [SerializeField] private ScoreCollector scoreCollector;
        [SerializeField] private RestartButton restartButton1;
        [SerializeField] private RestartButton restartButton2;

        public override void InstallBindings()
        {
            Container.Bind<BallsSpawner>().FromInstance(ballsSpawner).AsSingle();
            Container.Bind<Cup>().FromInstance(cup).AsSingle();
            Container.Bind<CupDropController>().FromInstance(cupDropController).AsSingle();
            Container.Bind<CupMovementController>().FromInstance(cupMovementController).AsSingle();

            Container.Bind<LevelsController>().FromInstance(levelsController).AsSingle();
            Container.Bind<GameEventsInvoker>().FromInstance(gameEventsInvoker).AsSingle();
            Container.Bind<ScoreCollector>().FromInstance(scoreCollector).AsSingle();
            
            Container.Bind<CupCatcher>().FromInstance(cupCatcher).AsSingle();
            Container.Bind<GlassCupCatcher>().FromInstance(glassCupCatcher).AsSingle();

            Container.Bind<Stars>().FromInstance(stars).AsSingle();

            Container.Bind<RestartButton>().FromInstance(restartButton1).AsCached();
            Container.Bind<RestartButton>().FromInstance(restartButton2).AsCached();
        }
    }
}