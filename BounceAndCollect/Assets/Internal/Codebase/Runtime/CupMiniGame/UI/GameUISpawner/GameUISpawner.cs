using System;
using Internal.Codebase.Infrastructure.Services.PercentSpeedometer;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.GameUISpawner
{
    [DisallowMultipleComponent]
    public sealed class GameUISpawner : MonoBehaviour
    {
        private SpeedometerService speedometerService;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(SpeedometerService speedometerService, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.speedometerService = speedometerService;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnGotThreeStars += ShowSpeedometer;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnGotThreeStars -= ShowSpeedometer;
        }

        private void ShowSpeedometer()
        {
            speedometerService.Init();
            speedometerService.ShowSpeedometer();
        }
    }
}