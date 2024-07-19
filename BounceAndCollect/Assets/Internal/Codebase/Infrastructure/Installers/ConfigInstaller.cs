using Internal.Codebase.Infrastructure.UI.LoadingCurtain;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer;
using Internal.Codebase.Runtime.MetaGame.GameData;
using Internal.Codebase.Runtime.MetaGame.Shop;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Installers
{
    [DisallowMultipleComponent]
    public sealed class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private CurtainConfig curtainConfig;
        [SerializeField] private MultiplierConfig multiplierConfig;
        
        [Header("BallsSkins")]
        [SerializeField] private BallsSkinsConfig defaultSkinsConfig;
        [SerializeField] private BallsSkinsConfig digitalCircusSkinsConfig;
        
        [SerializeField] private GameData gameData;
        [SerializeField] private SpeedometerConfig speedometerConfig;

        public override void InstallBindings()
        {
            Container.Bind<CurtainConfig>().FromInstance(curtainConfig).AsSingle();
            Container.Bind<MultiplierConfig>().FromInstance(multiplierConfig).AsSingle();

            Container.Bind<BallsSkinsConfig>().FromInstance(defaultSkinsConfig);
            Container.Bind<BallsSkinsConfig>().FromInstance(digitalCircusSkinsConfig);

            Container.Bind<GameData>().FromInstance(gameData).AsSingle();

            Container.Bind<SpeedometerConfig>().FromInstance(speedometerConfig).AsSingle();
        }
    }
}