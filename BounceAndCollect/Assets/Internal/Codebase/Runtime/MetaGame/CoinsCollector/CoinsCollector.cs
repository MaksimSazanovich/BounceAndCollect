using System;
using Internal.Codebase.Runtime.CupMiniGame.UI.Particles;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MetaGame.CoinsCollector
{
    [DisallowMultipleComponent]
    public sealed class CoinsCollector : MonoBehaviour
    {
        private int coins;
        [SerializeField] private Text text;

        private void OnEnable()
        {
            TweenParticle.OnFinished += AddCoin;
        }

        private void OnDisable()
        {
            TweenParticle.OnFinished -= AddCoin;
        }

        private void AddCoin()
        {
            coins++;
            text.text = coins.ToString();
        }
    }
}