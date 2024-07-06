using Internal.Codebase.Infrastructure.Factories.BallsFactory;
using Internal.Codebase.Infrastructure.Factories.BoosterLinesFactory;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using Internal.Codebase.Infrastructure.Factories.MainUIFactory;
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
            Container.Bind<BoosterLinesFactory>().AsSingle().NonLazy();
            Container.Bind<LevelTemplateFactory>().AsSingle().NonLazy();
        }
    }
}