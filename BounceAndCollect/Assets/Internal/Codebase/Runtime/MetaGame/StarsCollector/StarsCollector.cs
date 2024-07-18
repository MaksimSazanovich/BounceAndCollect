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
            StarParticle.OnFinished += AddStar;
        }

        private void OnDisable()
        {
            StarParticle.OnFinished -= AddStar;
        }

        private void AddStar()
        {
            stars++;
            text.text = stars.ToString();
        }
    }
}