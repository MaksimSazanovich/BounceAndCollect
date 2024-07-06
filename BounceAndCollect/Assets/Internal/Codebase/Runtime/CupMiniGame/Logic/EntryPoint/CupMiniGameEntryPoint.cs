using System;
using Internal.Codebase.Infrastructure.Factories.LevelTemplatesFactory;
using UnityEngine;
using Zenject;
using Types = Internal.Codebase.Runtime.CupMiniGame.LevelTemplate.Types;

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
            levelTemplateFactory.CreateLevel(Types.First, Vector3.zero,transform);
        }
    }
}