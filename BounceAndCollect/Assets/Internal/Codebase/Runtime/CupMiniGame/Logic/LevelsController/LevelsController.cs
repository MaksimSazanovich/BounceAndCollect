using System;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using Internal.Codebase.Runtime.CupMiniGame.Cup;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
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

        public LevelTemplate.LevelTemplate firstPart { get; private set; } = null;
        private LevelTemplate.LevelTemplate secondPart = null;

        public Action<LevelParts> OnChangePart;
        private GlassCupCatcher glassCupCatcher;

        [field: SerializeField] public LevelParts CurrentPart { get; private set; }

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker, LevelTemplateFactory levelTemplateFactory,
            CupMovementController cupMovementController, GlassCupCatcher glassCupCatcher)
        {
            this.glassCupCatcher = glassCupCatcher;
            this.cupMovementController = cupMovementController;
            this.levelTemplateFactory = levelTemplateFactory;
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void Start()
        {
            CurrentPart = LevelParts.First;
            Restart();
        }

        private void OnEnable()
        {
            cupMovementController.OnReplaced += SetSecondPart;

            gameEventsInvoker.OnEndedPart += CreateSecondPart;
            gameEventsInvoker.OnRestart += SetFirstPart;
            gameEventsInvoker.OnRestart += Restart;
            glassCupCatcher.OnOverflowed += SetThirdPart;
        }

        private void OnDisable()
        {
            cupMovementController.OnReplaced -= SetSecondPart;

            gameEventsInvoker.OnEndedPart -= CreateSecondPart;
            gameEventsInvoker.OnRestart -= SetFirstPart;
            gameEventsInvoker.OnRestart -= Restart;
            glassCupCatcher.OnOverflowed -= SetThirdPart;
        }

        private void SetFirstPart()
        {
            CurrentPart = LevelParts.First;
            OnChangePart?.Invoke(CurrentPart);
        }

        private void SetSecondPart()
        {
            CurrentPart = LevelParts.Second;
            OnChangePart?.Invoke(CurrentPart);
        }

        private void SetThirdPart()
        {
            CurrentPart = LevelParts.Third;
            OnChangePart?.Invoke(CurrentPart);
        }

        private void CreateFirstPart()
        {
            firstPart = levelTemplateFactory.CreateLevel(LevelTemplateTypes.First, Vector3.zero,transform);
        }

        private void CreateSecondPart()
        {
            secondPart = levelTemplateFactory.CreateLevel(LevelTemplateTypes.Second, secondHalfPosition, transform);
        }

        private void DestroyLevel()
        {
            Destroy(firstPart.gameObject);
            Destroy(secondPart.gameObject);
            BoosterLine[] boosterLines = FindObjectsOfType<BoosterLine>();
            foreach (var boosterLine in boosterLines)
            {
                Destroy(boosterLine.gameObject);
            }
        }

        private void Restart()
        {
            SetFirstPart();
            if(firstPart != null || secondPart != null)
                DestroyLevel();
            CreateFirstPart();
        }
    }
}