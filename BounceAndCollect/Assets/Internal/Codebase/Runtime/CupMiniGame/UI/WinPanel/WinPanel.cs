using System.Collections;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel
{
    [DisallowMultipleComponent]
    public sealed class WinPanel : MonoBehaviour
    {
        private GameEventsInvoker gameEventsInvoker;
        [SerializeField] private GameObject winPanel;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += () => StartCoroutine(Activate());
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= () => StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            yield return new WaitForSeconds(1);
            winPanel.SetActive(true);
        }
    }
}