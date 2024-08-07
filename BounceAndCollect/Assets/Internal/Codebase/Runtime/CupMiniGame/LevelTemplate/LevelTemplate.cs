using System.Collections.Generic;
using Internal.Codebase.Infrastructure.Factories.BoosterLinesFactory;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines;
using Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers;
using NaughtyAttributes;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.CupMiniGame.LevelTemplate
{
    [DisallowMultipleComponent]
    public sealed class LevelTemplate : MonoBehaviour
    {
        [SerializeField] private BoosterLine[] boosterLines;
        [SerializeField] private MultiplierX[] multipliersX;
        private BoosterLinesFactory boosterLinesFactory;

        [Inject]
        private void Constructor(BoosterLinesFactory boosterLinesFactory)
        {
            this.boosterLinesFactory = boosterLinesFactory;
        }

        [Button]
        public void Flip()
        {
            transform.localScale = new(transform.localScale.x * -1, 1, 1);
            foreach (BoosterLine boosterLine in boosterLines)
            {
                boosterLine.transform.localScale = new(boosterLine.transform.localScale.x * -1, 1, 1);
            }
        }
        
        public void SetSecondType()
        {
            HashSet<MultiplierX> futureSubtractors = new();
            for (int i = 0; i < multipliersX.Length - 1; i++)
            {
                futureSubtractors.Add(multipliersX[Random.Range(0, multipliersX.Length)]);
            }

            foreach (var futureSubtractor in futureSubtractors)
            {
                futureSubtractor.gameObject.SetActive(false);
                boosterLinesFactory.CreateSubtractor(futureSubtractor.GetComponent<SpriteRenderer>().size,
                    futureSubtractor.transform, transform);
            }
        }
    }
}