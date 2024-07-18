using Internal.Codebase.Runtime.CupMiniGame.UI.Particles;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MetaGame.StarsCollector
{
    [DisallowMultipleComponent]
    public sealed class StarsCollector : MonoBehaviour
    {
        private int stars;
        [SerializeField] private Text text;

        private void OnEnable()
        {
            StarParticle.OnFinished += AddCoin;
        }

        private void OnDisable()
        {
            StarParticle.OnFinished -= AddCoin;
        }

        private void AddCoin()
        {
            stars++;
            text.text = stars.ToString();
        }
    }
}