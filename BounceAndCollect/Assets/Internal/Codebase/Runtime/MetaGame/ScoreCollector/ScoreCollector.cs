using UnityEngine;

namespace Internal.Codebase.Runtime.MetaGame.ScoreCollector
{
    public sealed class ScoreCollector : MonoBehaviour
    {
        private uint score;

        private void AddScore(uint score) => this.score += score;
    }
}