using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Infrastructure.Services.LoadingCurtain;
using Internal.Codebase.Infrastructure.Services.SceneLoader;
using Internal.Codebase.Infrastructure.UI.LoadingCurtain;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Infrastructure.GameBootstrapper
{
    public class GameBootstrapper : MonoBehaviour
    {
        private SceneLoaderService loaderService;
        private CurtainService curtainService;
        private CurtainConfig curtainConfig;

        [Inject]
        private void Constructor(SceneLoaderService loaderService, CurtainService curtainService,
            CurtainConfig curtainConfig)
        {
            this.curtainConfig = curtainConfig;
            this.curtainService = curtainService;
            this.loaderService = loaderService;
        }
        private void Start()
        {
            Load();
        }

        private void Load()
        {
            curtainService.Init();
            curtainService.ShowCurtain(true, HideCurtain); 
        }

        private void HideCurtain()
        {
            curtainService.HideCurtain(curtainConfig.HideDelay);
            loaderService.LoadScene(SceneName.GameScene, () => YandexGame.InitGRA());
        }
    }
}
