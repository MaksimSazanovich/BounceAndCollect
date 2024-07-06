using Internal.Codebase.Infrastructure.Services.CoroutineRunner;
using Internal.Codebase.Infrastructure.Services.LoadingCurtain;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using UnityEngine;
using Zenject;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;


namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class ServiceInstaller : MonoInstaller, ICoroutineRunner
    {
        public override void InstallBindings()
        {
            Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle().NonLazy();
            Container.Bind<SceneLoaderService>().AsSingle().NonLazy();
            
            Container.Bind<ResourceProvider>().AsSingle().NonLazy();
            Container.Bind<SkinsResourceProvider>().AsSingle().NonLazy();
            Container.Bind<BoosterLinesResourceProvider>().AsSingle().NonLazy();
            Container.Bind<LevelTemplatesResourceProvider>().AsSingle().NonLazy();
            
            Container.Bind<CurtainService>().AsSingle().NonLazy();
        }
    }
}