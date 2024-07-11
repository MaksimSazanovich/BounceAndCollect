using System;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.CupMiniGame.LevelTemplate;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic.LevelsController
{
    [DisallowMultipleComponent]
    public sealed class LevelsController : MonoBehaviour
    {
        private GameEventsInvoker gameEventsInvoker;
        private LevelTemplateFactory levelTemplateFactory;
        private Vector3 secondHalfPosition = new(0, -10);
        private CupMovementController cupMovementController;

        public Action<LevelParts> OnChangePart; 

        [field: SerializeField] public LevelParts CurrentPart { get; private set; }

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker, LevelTemplateFactory levelTemplateFactory,
            CupMovementController cupMovementController)
        {
            this.cupMovementController = cupMovementController;
            this.levelTemplateFactory = levelTemplateFactory;
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void Start()
        {
            CurrentPart = LevelParts.First;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += () =>
                levelTemplateFactory.CreateLevel(LevelTemplateTypes.Second, secondHalfPosition, transform);

            cupMovementController.OnReplaced += SetSecondPart;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= () =>
                levelTemplateFactory.CreateLevel(LevelTemplateTypes.Second, secondHalfPosition, transform);

            cupMovementController.OnReplaced -= SetSecondPart;
        }

        private void SetSecondPart()
        {
            CurrentPart = LevelParts.Second;
            OnChangePart?.Invoke(CurrentPart);
        }
    }
}