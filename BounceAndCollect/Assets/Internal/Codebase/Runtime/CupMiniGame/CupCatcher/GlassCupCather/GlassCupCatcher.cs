using System;
using Internal.Codebase.Runtime.CupMiniGame.Ball;
using NTC.Pool;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.CupCatcher.GlassCupCather
{
    [DisallowMultipleComponent]
    public sealed class GlassCupCatcher : Cathcer
    {
        public Action OnOverflowed;
        public Action OnOffsetReplaced;
        public Action OnAddedCaughtBall;
        
        private Collider2D collider2D;
        private float offset = 0.052f;

        [SerializeField] private GameObject glassCup;
        
        private void Start()
        {
            collider2D = GetComponent<Collider2D>();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                if (CaughtBalls <= 200)
                {
                    if (CaughtBalls <= 50)
                    {
                        transform.position = new(transform.position.x, transform.position.y - offset, 0);
                        OnOffsetReplaced?.Invoke();
                    }
                    else
                    {
                        collider2D.enabled = false;
                        collider2D.enabled = true;
                        NightPool.Despawn(ballCollision.gameObject);
                    }

                    particle.Play();
                }
                else
                {
                    OnOverflowed?.Invoke();
                    Explode();
                }
                
                caughtBallsText.text = $"{CaughtBalls}/50";
            }
        }

        protected override void AddCaughtBall()
        {
            base.AddCaughtBall();
            OnAddedCaughtBall?.Invoke();
        }

        private void Explode()
        {
            glassCup.SetActive(false);
        }
    }
}