using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Runtime.Shop;
using Internal.Codebase.Runtime.Shop.Skins;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.ResourceProvider
{
    public sealed class SkinsResourceProvider
    {
        public BallsSkinsConfig LoadBallsSkinsConfig(BallsSkins skin)
        {
            BallsSkinsConfig ballsSkinsConfig = Resources.Load<BallsSkinsConfig>(AssetPath.BallsSkinsConfig + skin);
            return ballsSkinsConfig;
        }
        public void UnloadAsset(Object asset) => Resources.UnloadAsset(asset);
    }
}