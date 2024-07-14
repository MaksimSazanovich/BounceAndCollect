using Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.PercentSpeedometer
{
    public sealed class SpeedometerService
    {
        private GameUIFactory.GameUIFactory gameUIFactory;
        private Speedometer speedometer;

        [Inject]
        private void Constructor(GameUIFactory.GameUIFactory gameUIFactory) =>
            this.gameUIFactory = gameUIFactory;

        public void Init() =>
            speedometer = gameUIFactory.CreateSpeedometer();

        public void ShowSpeedometer() =>
            speedometer.Show();

        public void HideSpeedometer() =>
            speedometer.Hide();
    }
}