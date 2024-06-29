using Internal.Codebase.Runtime.CupMiniGame.Ball;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Subtractor
{
    [DisallowMultipleComponent]
    public sealed class Subtractor : BoosterLine
    {
        private int[] values = { -10, -15, -20 };
        private int value;
        [SerializeField] private Text valueText;

        private void Start()
        {
            value = values[Random.Range(0, values.Length-1)];
            valueText.text = value.ToString();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ball))
            {
                value += 1;
                valueText.text = value.ToString();
                if(value >= 0)
                    Destroy(gameObject);
                ball.Lock(this);
            }
        }
    }
}