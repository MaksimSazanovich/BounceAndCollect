using System;
using Internal.Codebase.Runtime.CupMiniGame.Logic.GameEvents;
using UnityEngine;
using YG;
using Zenject;

namespace Internal.Codebase.Runtime.CupMiniGame.UI.WinPanel
{
    [DisallowMultipleComponent]
    public sealed class TestLb : MonoBehaviour
    {
        [SerializeField] private LeaderboardYG leaderboardYG;
        private int i;
        private GameEventsInvoker gameEventsInvoker;

        [Inject]
        private void Constructor(GameEventsInvoker gameEventsInvoker)
        {
            this.gameEventsInvoker = gameEventsInvoker;
        }

        private void OnEnable()
        {
            gameEventsInvoker.OnWon += AddPlayerToLb;
        }

        private void OnDisable()
        {
            gameEventsInvoker.OnWon -= AddPlayerToLb;
        }

        public void AddPlayerToLb()
        {
            YandexGame.NewLeaderboardScores(leaderboardYG.nameLB, i++);
            leaderboardYG.UpdateLB();
        }
    }
}