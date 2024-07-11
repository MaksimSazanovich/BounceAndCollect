using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather;
using Internal.Codebase.Runtime.UI.Animations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.Stars
{
    [DisallowMultipleComponent]
    public sealed class Stars : MonoBehaviour
    {
        private GlassCupCatcher glassCupCatcher;
        private float starsFillAmount = 0.02f;
        [SerializeField] private Vector3[] starsPositions;
        [SerializeField] private Image[] stars;
        [SerializeField] private GameObject glow;
        [SerializeField] private ParticleSystem particle;
        private float glowTime = 0.1f;

        [Inject]
        private void Constructor(GlassCupCatcher glassCupCatcher)
        {
            this.glassCupCatcher = glassCupCatcher;
        }

        private void OnEnable()
        {
            glassCupCatcher.OnAddedCaughtBall += Fill;
        }

        private void OnDisable()
        {
            glassCupCatcher.OnAddedCaughtBall -= Fill;
        }

        private void Start()
        {
            glow.SetActive(false);

            foreach (var star in stars)
            {
                star.fillAmount = 0;
            }
        }

        private void Fill()
        {
            if (glassCupCatcher.CaughtBalls is 50 or 150 or 200)
            {
                int i = glassCupCatcher.CaughtBalls / 100;
                StartCoroutine(ShowGlow(starsPositions[i]));
                stars[i].GetComponent<UIShakeAnimation>().Play();
                PlayParticle(starsPositions[i]);
                stars[i].color = Color.yellow;
            }
            
            if (glassCupCatcher.CaughtBalls <= 50)
            {
                stars[0].fillAmount += starsFillAmount;
                return;
            }

            if (glassCupCatcher.CaughtBalls <= 150)
            {
                stars[1].fillAmount += 0.01f;
                return;
            }

            if (glassCupCatcher.CaughtBalls <= 200)
                stars[2].fillAmount += starsFillAmount;

           
        }
        
        private IEnumerator ShowGlow(Vector3 starPosition)
        {
            glow.SetActive(true);
            glow.transform.localPosition = starPosition;
            yield return new WaitForSeconds(glowTime);
            glow.SetActive(false);
        }

        private void PlayParticle(Vector3 starPosition)
        {
            particle.transform.localPosition = starPosition;
            particle.Play();
        }
    }
}