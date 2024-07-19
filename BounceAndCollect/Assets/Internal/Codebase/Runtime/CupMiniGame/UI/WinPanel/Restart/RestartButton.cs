using System;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel.Restart
{
    [DisallowMultipleComponent]
    public sealed class RestartButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        public Action OnClicked;

        private void OnEnable()
        {
            button.onClick.AddListener(InvokeClick);
        }

        private void InvokeClick()
        {
            OnClicked?.Invoke();
        }
    }
}