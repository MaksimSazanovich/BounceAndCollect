using Internal.Codebase.Infrastructure.Factories.MultipliersFactory;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;

namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers
{
    [DisallowMultipleComponent]
    public sealed class RandomMultiplierX : BoosterLine
    {
        private MultipliersFactory multipliersFactory;
        private ResourceProvider resourceProvider;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        [Inject]
        private void Constructor(MultipliersFactory multipliersFactory, ResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
            this.multipliersFactory = multipliersFactory;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                int randomValue = Random.Range(resourceProvider.LoadMultipliersConfig().MinValue,
                    resourceProvider.LoadMultipliersConfig().MaxValue);
                
                multipliersFactory.CreateMultiplierX(randomValue, spriteRenderer.size, transform.position);
                gameObject.SetActive(false);
            }
        }
    }
}