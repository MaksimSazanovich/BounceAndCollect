using Internal.Codebase.Infrastructure.Factories.BallsFactory;
using Internal.Codebase.Infrastructure.Factories.MainUIFactory;
using Internal.Codebase.Infrastructure.Factories.MultipliersFactory;
using Internal.Codebase.Infrastructure.Factories.SkinsFactories;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class FactoryInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MainUIFactory>().AsSingle().NonLazy();
            Container.Bind<BallsFactory>().AsSingle().NonLazy();
            Container.Bind<MultipliersFactory>().AsSingle().NonLazy();
            Container.Bind<BallsSkinsFactory>().AsSingle().NonLazy();
        }
    }
}