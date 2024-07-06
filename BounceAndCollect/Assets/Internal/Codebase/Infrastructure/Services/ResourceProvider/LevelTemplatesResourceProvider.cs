using Internal.Codebase.Infrastructure.Constants;
using Internal.Codebase.Runtime.CupMiniGame.LevelTemplate;
using Internal.Codebase.Runtime.MetaGame.GameData;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Infrastructure.Services.ResourceProvider
{
    public sealed class LevelTemplatesResourceProvider
    {
        private GameData gameData;

        [Inject]
        private void Constructor(GameData gameData)
        {
            this.gameData = gameData;
        }

        public LevelTemplate LoadRandomTemplate()
        {
            return Resources.Load<LevelTemplate>(AssetPath.LevelsTemplate +
                                                 Random.Range(1, gameData.LevelTemplatesCount));
        }
        
        public void UnloadAsset(Object asset) => Resources.UnloadAsset(asset);
    }
}