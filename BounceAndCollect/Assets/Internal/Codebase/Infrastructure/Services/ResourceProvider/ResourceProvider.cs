using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Infrastructure.UI.LoadingCurtain;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.ResourceProvider
{
    public sealed class ResourceProvider
    {
        private CurtainConfig curtainConfig;
        private BallConfig ballConfig;
        private SpeedometerConfig speedometerConfig;

        public CurtainConfig LoadCurtainConfig()
        {
            if (curtainConfig == null)
                curtainConfig = Resources.Load<CurtainConfig>(AssetPath.CurtainConfig);
            return curtainConfig;
        }

        public BallConfig LoadBallConfig()
        {
            if (ballConfig == null)
                ballConfig = Resources.Load<BallConfig>(AssetPath.BallConfig);
            return ballConfig;
        }
        
        public SpeedometerConfig LoadSpeedometerConfig()
        {
            if (speedometerConfig == null)
                speedometerConfig = Resources.Load<SpeedometerConfig>(AssetPath.SpeedometerConfig);
            return speedometerConfig;
        }

        public void UnloadAsset(Object asset) => Resources.UnloadAsset(asset);
    }
}