using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Subtractor
{
    [CreateAssetMenu(fileName = "SutractorConfig", menuName = "StaticData/Create SutractorConfig", order = 4)]
    public class SubtractorConfig : ScriptableObject
    {
        [field: SerializeField] public Subtractor Subtractor { get; private set; }
    }
}