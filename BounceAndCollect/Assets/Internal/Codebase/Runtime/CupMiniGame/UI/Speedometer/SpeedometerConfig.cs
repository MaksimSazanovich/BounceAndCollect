using System;
using DG.Tweening;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Speedometer
{
    [CreateAssetMenu(menuName = "StaticData/Create SpeedometerConfig", fileName = "SpeedometerConfig", order = 6)]
    public class SpeedometerConfig : ScriptableObject
    {
        [field: SerializeField] public UI.Speedometer.Speedometer Speedometer { get; private set; }

        [field: Space(20)]
        [field: SerializeField] public int MinValue { get; private set; } = 250;

        [field: SerializeField] public int Step { get; private set; }

        [field: SerializeField] public int MaxValue { get; private set; } = 1500;

        [field: Space(20)]
        [field: SerializeField] public float AnimationDuration { get; private set; } = 1.5f;

        [field: SerializeField] public Ease Ease { get; private set; }

        [field: SerializeField] public Vector3 EndScale { get; private set; }

        private void OnValidate()
        {
            MaxValue = Step * 70 + MinValue;
        }
    }
}