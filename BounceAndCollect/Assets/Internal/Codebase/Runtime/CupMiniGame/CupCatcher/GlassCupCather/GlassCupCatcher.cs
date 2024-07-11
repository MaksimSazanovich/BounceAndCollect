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
        
        private float offset = 0.052f;

        [SerializeField] private GameObject glassCup;
        [SerializeField] private ParticleSystem explosion;
        private BoxCollider2D boxCollider2D;
        private float boxCollider2DEndPos = 1.1f;
        
        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.TryGetComponent(out BallCollision ballCollision))
            {
                if (CaughtBalls < 200)
                {
                    if (CaughtBalls <= 50)
                    {
                        transform.position = new(transform.position.x, transform.position.y - offset, 0);
                        OnOffsetReplaced?.Invoke();
                    }
                    else
                    {
                        boxCollider2D.enabled = false;
                        boxCollider2D.enabled = true;
                        NightPool.Despawn(ballCollision.gameObject);
                    }

                    particle.Play();
                }
                else if(CaughtBalls == 200)
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
            explosion.Play();
            glassCup.SetActive(false);
            boxCollider2D.offset = new(boxCollider2D.offset.x, boxCollider2DEndPos);
        }
    }
}