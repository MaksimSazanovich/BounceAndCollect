using Internal.Codebase.Runtime.CupMiniGame.Ball;
using UnityEngine;

namespace Internal.Codebase.Runtime.CupMiniGame.BoosterLines.Multipliers.HorizontalMovingPlatform
{
    public sealed class HorizontalMovingPlatform : MonoBehaviour
    {
        [SerializeField] private float leftPointX;
        [SerializeField] private float rightPointX;

        [SerializeField] private float speed = 1;
        [SerializeField] private float stopTime = 1;

        [SerializeField] private Points startPoint;


        private bool canMove = true;

        private Vector3 currentTarget;
        private Vector3 leftPoint;
        private Vector3 rightPoint;

        private float stopTimer;
        private float currentTime;

        private void Awake()
        {
            leftPoint = new Vector3(leftPointX, transform.position.y);
            rightPoint = new Vector3(rightPointX, transform.position.y);

            if (startPoint == Points.leftPoint)
            {
                currentTarget = rightPoint;
                transform.position = leftPoint;
            }
            else
            {
                currentTarget = leftPoint;
                transform.position = rightPoint;
            }

            stopTimer = stopTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out BallCollision ballCollision) &&
                transform.position.y <= other.transform.position.y)
                Stop();
        }

        private void FixedUpdate()
        {
            if (canMove)
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, currentTarget, speed * Time.fixedDeltaTime);


                if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
                {
                    stopTimer -= Time.fixedDeltaTime;

                    if (stopTimer <= 0f)
                    {
                        currentTarget = (currentTarget == leftPoint) ? rightPoint : leftPoint;
                        stopTimer = stopTime;
                        
                        currentTime = 0;
                    }
                }
            }
        }

        private void Stop() => canMove = false;
    }
}