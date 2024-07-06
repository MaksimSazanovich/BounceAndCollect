using System;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;
using Types = Internal.Codebase.Runtime.CupMiniGame.LevelTemplate.Types;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.LevelsController
{
    [DisallowMultipleComponent]
    public sealed class LevelsController : MonoBehaviour
    {
        private GameEventsInvoker gameEventsInvoker;
        private LevelTemplateFactory levelTemplateFactory;
        private Vector3 secondHalfPosition = new(0,-10);

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker, LevelTemplateFactory levelTemplateFactory)
        {
            this.levelTemplateFactory = levelTemplateFactory;
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += () => levelTemplateFactory.CreateLevel(Types.Second, secondHalfPosition, transform);
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= () => levelTemplateFactory.CreateLevel(Types.Second, secondHalfPosition, transform);
        }
    }
}