using System;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using Internal.Codebase.Runtime.CupMiniGame.LevelTemplate;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.Logic
{
    [DisallowMultipleComponent]
    public sealed class CupMiniGameEntryPoint : MonoBehaviour
    {
        private LevelTemplateFactory levelTemplateFactory;

        [Inject]
        private void Constructor(LevelTemplateFactory levelTemplateFactory)
        {
            this.levelTemplateFactory = levelTemplateFactory;
        }

        private void Start()
        {
            levelTemplateFactory.CreateLevel(LevelTemplateTypes.First, Vector3.zero,transform);
        }
    }
}