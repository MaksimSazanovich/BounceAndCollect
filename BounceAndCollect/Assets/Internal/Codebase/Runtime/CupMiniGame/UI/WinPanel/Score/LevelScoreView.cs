using System;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using Internal.Codebase.Runtime.MetaGame.ScoreCollector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel.Score
{
    [DisallowMultipleComponent]
    public sealed class LevelScoreView : MonoBehaviour
    {
        private ScoreCollector scoreCollector;
        [SerializeField] private Text text;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(ScoreCollector scoreCollector, GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
            this.scoreCollector = scoreCollector;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnEnded += ChangeText;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnEnded -= ChangeText;
        }

        private void ChangeText()
        {
            text.text = scoreCollector.LevelScore.ToString();
        }
    }
}