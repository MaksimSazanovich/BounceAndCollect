using System;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using UnityEngine.WSA;
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
            gameEventsInvoker.OnEnded += Activate;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= Activate;
        }

        private void Activate()
        {
            winPanel.SetActive(true);
        }
    }
}