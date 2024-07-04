using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.Shop;
using Internal.Codebase.Runtime.UI.MainUI.LoadingCurtain;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private CurtainConfig curtainConfig;
        [SerializeField] private BallConfig ballConfig;
        [FormerlySerializedAs("multipliersConfig")] [SerializeField] private MultiplierConfig multiplierConfig;
        
        [Header("BallsSkins")]
        [SerializeField] private BallsSkinsConfig defaultSkinsConfig;
        [SerializeField] private BallsSkinsConfig digitalCircusSkinsConfig;

        public override void InstallBindings()
        {
            Container.Bind<CurtainConfig>().FromInstance(curtainConfig).AsSingle();
            Container.Bind<BallConfig>().FromInstance(ballConfig).AsSingle();
            Container.Bind<MultiplierConfig>().FromInstance(multiplierConfig).AsSingle();

            Container.Bind<BallsSkinsConfig>().FromInstance(defaultSkinsConfig);
            Container.Bind<BallsSkinsConfig>().FromInstance(digitalCircusSkinsConfig);
        }
    }
}