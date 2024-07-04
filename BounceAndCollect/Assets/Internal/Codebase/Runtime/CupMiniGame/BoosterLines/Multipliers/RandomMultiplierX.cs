using Internal.Codebase.Infrastructure.Factories.BoosterLinesFactory;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Utilities;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;

namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers
{
    [DisallowMultipleComponent]
    public sealed class RandomMultiplierX : BoosterLine
    {
        private BoosterLinesFactory boosterLinesFactory;
        private BoosterLinesResourceProvider resourceProvider;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [Inject]
        private void Constructor(BoosterLinesFactory boosterLinesFactory, BoosterLinesResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
            this.boosterLinesFactory = boosterLinesFactory;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                int randomValue = HierarchyRandom.Range(resourceProvider.LoadMultiplierConfig().MinValue,
                    resourceProvider.LoadMultiplierConfig().MaxValue);

                boosterLinesFactory.CreateMultiplierX(randomValue, spriteRenderer.size, transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}