using UnityEngine;

namespace Internal.Codebase.Runtime.MetaGame.GameData
{
    [CreateAssetMenu(fileName = "GameData", menuName = "StaticData/Create GameData", order = 5)]
    public class GameData : ScriptableObject
    {
        [field: SerializeField] public int LevelTemplatesCount { get; private set; }
        [field: SerializeField] public int BallsOnStart { get; private set; } = 3;
    }
}