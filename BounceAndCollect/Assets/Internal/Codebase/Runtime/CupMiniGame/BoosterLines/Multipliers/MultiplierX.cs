using System;
using System.Collections;
using Internal.Codebase.Infrastructure.Services.ResourceProvider;
using Internal.Codebase.Runtime.Constants;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using Internal.Codebase.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers
{
    [DisallowMultipleComponent]
    public class MultiplierX : BoosterLine, IEnumerable
    {
        private BoosterLinesResourceProvider resourceProvider;
        [field: SerializeField] public int Value { get; private set; }

        [Inject]
        private void Constructor(BoosterLinesResourceProvider resourceProvider)
        {
            this.resourceProvider = resourceProvider;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            Init(Value);
        }

        private void Start()
        {
            int randomValue = HierarchyRandom.Range(resourceProvider.LoadMultiplierConfig().MinValue,
                resourceProvider.LoadMultiplierConfig().MaxValue);
            Value = randomValue;
            
            Init(Value);
        }

        public void Init(int value)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            Text valueText = GetComponentInChildren<Text>();

            string title = $"X{value}";
            valueText.text = title;
            gameObject.name = title;

            switch (value)
            {
                case 2:
                    spriteRenderer.color = Colors.x2;
                    break;
                case 3:
                    spriteRenderer.color = Colors.x2;
                    break;
                case 4:
                    spriteRenderer.color = Colors.x4;
                    break;
                case 5:
                    spriteRenderer.color = Colors.x5;
                    break;
                case 6:
                    spriteRenderer.color = Colors.x5;
                    break;
                case 7:
                    spriteRenderer.color = Colors.x5;
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision) &&
                transform.position.y < other.transform.position.y && !ballCollision.LockBoosterLineIDs.Contains(ID))
            {
                //ballCollision.LockMultiplierX(this);
                ballCollision.Lock(this);
            }
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}