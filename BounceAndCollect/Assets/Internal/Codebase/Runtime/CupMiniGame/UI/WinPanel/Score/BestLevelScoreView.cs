using Internal.Codebase.Runtime.MetaGame.ScoreCollector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel.Score
{
    [DisallowMultipleComponent]
    public sealed class BestLevelScoreView : MonoBehaviour
    {
        private ScoreCollector scoreCollector;
        [SerializeField] private Text text;

        [Inject]
        private void Constructor(ScoreCollector scoreCollector)
        {
            this.scoreCollector = scoreCollector;
        }

        private void Start()
        {
            text.text = scoreCollector.BestLevelScore.ToString();
        }
    }
}