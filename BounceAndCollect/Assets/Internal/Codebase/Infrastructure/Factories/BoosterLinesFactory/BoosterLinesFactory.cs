using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Subtractor;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Factories.BoosterLinesFactory
{
    public sealed class BoosterLinesFactory
    {
        private BoosterLinesResourceProvider boosterLinesResourceProvider;
        private DiContainer container;
        private Subtractor subtractorComponent;
        private MultiplierX multiplierXComponent;

        [Inject]
        private void Constructor(BoosterLinesResourceProvider boosterLinesResourceProvider, DiContainer container)
        {
            this.container = container;
            this.boosterLinesResourceProvider = boosterLinesResourceProvider;
        }

        public MultiplierX CreateMultiplierX(int value, Vector2 size, Vector3 position)
        {
            var config = boosterLinesResourceProvider.LoadMultiplierConfig();
            
            var multiplierX = container.InstantiatePrefab(config.MultiplierX, position, Quaternion.identity, null);
            boosterLinesResourceProvider.UnloadAsset(config);
            
            multiplierXComponent = multiplierX.GetComponent<MultiplierX>();
            multiplierXComponent.Init(value);
            multiplierXComponent.SetSize(size);
            
            return multiplierXComponent;
        }

        public Subtractor CreateSubtractor(Vector2 size, Vector3 position, Transform parent)
        {
            var config = boosterLinesResourceProvider.LoadSubtractorConfig();
            
            var subtractor = container.InstantiatePrefab(config.Subtractor, position, Quaternion.identity, parent);
            boosterLinesResourceProvider.UnloadAsset(config);
            
            subtractorComponent = subtractor.GetComponent<Subtractor>();
            subtractorComponent.Init();
            subtractorComponent.SetSize(size);
            
            return subtractorComponent;
        }
    }
}