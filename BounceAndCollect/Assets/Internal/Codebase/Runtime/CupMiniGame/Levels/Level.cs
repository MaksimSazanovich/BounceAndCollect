using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using NaughtyAttributes;
using UnityEngine;


namespace Internal.Codebase.Runtime.CupMiniGame.Levels
{
    [DisallowMultipleComponent]
    public sealed class Level : MonoBehaviour
    {
        [SerializeField] private MultiplierX[] boosterLines;

        [Button]
        public void Flip()
        {
            transform.localScale = new(transform.localScale.x * -1, 1,1);
            foreach (MultiplierX boosterLine in boosterLines)
            {
                boosterLine.transform.localScale = new(boosterLine.transform.localScale.x * -1, 1,1);
            }
        }
    }
}