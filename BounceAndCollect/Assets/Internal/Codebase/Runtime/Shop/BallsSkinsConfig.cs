using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.Shop
{
    [CreateAssetMenu(menuName = "StaticData/Create BallsSkinsConfig", fileName = "BallsSkinsConfig", order = 3)]
    public class BallsSkinsConfig : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> Sprites { get; private set; }
    }
}