using Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer;
using UnityEngine;
using Zenject;
using ResourceProvider = Internal.Codebase.Infrastructure.Services.ResourceProvider.ResourceProvider;

namespace Internal.Codebase.Infrastructure.GameUIFactory
{
    public sealed class GameUIFactory
    {
        private ResourceProvider resourceProvider;

        [Inject]
        private void Constructor(ResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
        }

        public Speedometer CreateSpeedometer()
        {
            var config = resourceProvider.LoadSpeedometerConfig();
            var view = Object.Instantiate(config.Speedometer);
            view.Constructor(config.MinValue, config.MaxValue, config.AnimationDuration, config.Ease, config.EndScale);
            return view;
        }
    }
}