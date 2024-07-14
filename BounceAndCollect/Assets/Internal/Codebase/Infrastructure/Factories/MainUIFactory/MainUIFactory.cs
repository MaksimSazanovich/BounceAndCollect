using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Infrastructure.UI.LoadingCurtain;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;

namespace Internal.Codebase.Infrastructure.Factories.MainUIFactory
{
    public sealed class MainUIFactory
    {
        private GameObject currentPefab;
        private ResourceProvider resourceProvider;
        
        [Inject]
        private void Construct(ResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
        }
        
        public Curtain CreateCurtain()
        {
            var config = resourceProvider.LoadCurtainConfig();

            var view = Object.Instantiate(config.Curtain);
            resourceProvider.UnloadAsset(config);
            view.Constructor(config.AnimationDuration);

            return view;
        }
    }
}