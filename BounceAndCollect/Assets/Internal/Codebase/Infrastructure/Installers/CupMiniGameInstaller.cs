using Internal.Codebase.Runtime.CupMiniGame.BallSpawner;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.CupMiniGame.CupKeeper;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class CupMiniGameInstaller : MonoInstaller
    {
        [SerializeField] private BallsSpawner ballsSpawner;
        [SerializeField] private Cup cup;
        [SerializeField] private CupDropController cupDropController;
        [SerializeField] private CupKeeper cupKeeper;
        [SerializeField] private GameEventsInvoker gameEventsInvoker;

        public override void InstallBindings()
        {
            Container.Bind<BallsSpawner>().FromInstance(ballsSpawner).AsSingle();
            Container.Bind<Cup>().FromInstance(cup).AsSingle();
            Container.Bind<CupDropController>().FromInstance(cupDropController).AsSingle();
            Container.Bind<CupKeeper>().FromInstance(cupKeeper).AsSingle();
            Container.Bind<GameEventsInvoker>().FromInstance(gameEventsInvoker).AsSingle();
        }
    }
}