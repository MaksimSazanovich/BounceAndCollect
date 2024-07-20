using System;
using System.Collections;
using AssetKits.ParticleImage;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
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
        private Color grey;
        [SerializeField] private Image[] starsImages;
        [SerializeField] private UIShakeAnimation[] animations;
        [SerializeField] private ParticleImage[] particles;
        [SerializeField] private WinPanel.WinPanel winPanel;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(Stars stars, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.stars = stars;
        }

        private void Start()
        {
            grey = starsImages[0].color;
            Restart();
        }

        private void OnEnable()
        {
            winPanel.OnShowed += () => StartCoroutine(Fill());
            gameEventsInvoker.OnRestart += Restart;
        }

        private void OnDisable()
        {
            winPanel.OnShowed -= () => StartCoroutine(Fill());
            gameEventsInvoker.OnRestart -= Restart;
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

        private void Restart()
        {
            for (int i = 0; i < stars.StarsCount; i++)
            {
                starsImages[i].color = grey;
            }
        }
    }
}