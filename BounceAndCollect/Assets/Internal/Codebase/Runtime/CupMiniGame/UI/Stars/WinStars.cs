using System.Collections;
using AssetKits.ParticleImage;
using Internal.Codebase.Runtime.UI.Animations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Stars
{
    [DisallowMultipleComponent]
    public sealed class WinStars : MonoBehaviour
    {
        private Stars stars;
        [SerializeField] private Image[] starsImages;
        [SerializeField] private UIShakeAnimation[] animations;
        [SerializeField] private ParticleImage[] particles;

        [Inject]
        private void Constructor(Stars stars)
        {
            this.stars = stars;
        }

        private void OnEnable()
        {
            StartCoroutine(Fill());
        }

        private IEnumerator Fill()
        {
            for (int i = 0; i < stars.StarsCount; i++)
            {
                yield return new WaitForSeconds(0.2f);
                starsImages[i].color = Color.yellow;
                animations[i].Play();
                particles[i].Play();
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}