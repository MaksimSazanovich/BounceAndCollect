using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Subtractor;
using UnityEngine;

namespace Internal.Codebase.Infrastructure.Services.ResourceProvider
{
    public sealed class BoosterLinesResourceProvider
    {
        private MultiplierConfig multiplierConfig;
        private SubtractorConfig subtractorConfig;
        
        public MultiplierConfig LoadMultiplierConfig()
        {
            if (multiplierConfig == null)
                multiplierConfig = Resources.Load<MultiplierConfig>(AssetPath.MultipliersConfig);
            return multiplierConfig;
        }
        
        public SubtractorConfig LoadSubtractorConfig()
        {
            if (subtractorConfig == null)
                subtractorConfig = Resources.Load<SubtractorConfig>(AssetPath.SubtractorConfig);
            return subtractorConfig;
        }
        public void UnloadAsset(Object asset) => Resources.UnloadAsset(asset);
    }
}