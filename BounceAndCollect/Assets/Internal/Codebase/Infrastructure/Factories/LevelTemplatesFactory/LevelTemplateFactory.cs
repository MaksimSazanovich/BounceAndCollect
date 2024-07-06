using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.CupMiniGame.LevelTemplate;
using UnityEngine;
using Zenject;
using Types = Internal.Codebase.Runtime.CupMiniGame.LevelTemplate.Types;

namespace Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory
{
    public sealed class LevelTemplateFactory
    {
        private LevelTemplatesResourceProvider resourceProvider;
        private DiContainer container;

        [Inject]
        private void Constructor(LevelTemplatesResourceProvider resourceProvider, DiContainer container)
        {
            this.container = container;
            this.resourceProvider = resourceProvider;
        }
        
        public LevelTemplate CreateLevel(Types type, Vector3 position, Transform parent)
        {
            var resource = resourceProvider.LoadRandomTemplate();
            var levelTemplate = container.InstantiatePrefab(resource, position, Quaternion.identity, parent).GetComponent<LevelTemplate>();
            int randomValue = Random.Range(0, 2);
            if (randomValue == 0)
            {
                levelTemplate.Flip();
            }
            if(type == Types.Second)
                levelTemplate.SetSecondType();
            return levelTemplate;
        }
    }
}