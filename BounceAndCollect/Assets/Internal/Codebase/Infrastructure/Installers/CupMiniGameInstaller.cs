using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private GameEventsInvoker gameEventsInvoker;
        [SerializeField] private CupMovementController cupMovementController;

        public override void InstallBindings()
        {
            Container.Bind<BallsSpawner>().FromInstance(ballsSpawner).AsSingle();
            Container.Bind<Cup>().FromInstance(cup).AsSingle();
            Container.Bind<CupDropController>().FromInstance(cupDropController).AsSingle();
            Container.Bind<CupMovementController>().FromInstance(cupMovementController).AsSingle();
            Container.Bind<CupCatcher>().FromInstance(cupCatcher).AsSingle();
            Container.Bind<GameEventsInvoker>().FromInstance(gameEventsInvoker).AsSingle();
        }
    }
}