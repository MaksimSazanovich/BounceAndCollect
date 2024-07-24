using UnityEngine;
using YG;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel
{
    [DisallowMultipleComponent]
    public sealed class TestLb : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG leaderboardYG;
        private int i;
        
        public void AddPlayerToLb()
        {
            YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, i++);
            leaderboardYG.UpdateLB();
        }
    }
}