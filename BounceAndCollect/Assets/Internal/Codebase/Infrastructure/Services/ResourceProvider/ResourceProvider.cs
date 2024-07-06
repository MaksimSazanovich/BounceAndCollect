using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Runtime.UI.MainUI.LoadingCurtain;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.ResourceProvider
{
    public sealed class ResourceProvider
    {
        private CurtainConfig curtainConfig;
        private BallConfig ballConfig;

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

        public void UnloadAsset(Object asset) => Resources.UnloadAsset(asset);
    }
}