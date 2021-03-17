using System;
using System.Collections;
using UnityEngine;

namespace AzurProject.Movement
{
    [Serializable]
    public class LinearMovementCoordinate
    {
        public Vector3 position;
        public float duration = 1;
        public float waitTime;
    }
    
    public class LinearMovement : Movement
    {
        public LinearMovementCoordinate[] coordinates;
        
        [SerializeField] private float startDelay;
        [SerializeField] private bool loop;

        public override IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            do
            {
                for (int i = 0; i < coordinates.Length; i++)
                {
                    Vector3 point = coordinates[i].position;
                    float duration = coordinates[i].duration;
                    float waitTime = coordinates[i].waitTime;

                    StartCoroutine(LerpMoveCoroutine(point, duration));
                    yield return new WaitForSeconds(waitTime + duration);
                }
            } while (loop);
        }

        private IEnumerator LerpMoveCoroutine(Vector3 destination, float duration)
        {
            Vector3 currentPos = MainTransform.position;
            float t = 0f;
            while (t < 1)
            {
                t += Time.deltaTime / duration;
                MainTransform.position = Vector3.Lerp(currentPos, destination, t);
                yield return null;
            }
        }

        // For debugging purposes
        private void OnDrawGizmosSelected()
        {
            if (coordinates == null)
            {
                return;
            }
            
            Vector2 currentPosition = transform.position;
            Vector2 nextPosition;
            
            for (int i = 0; i < coordinates.Length; i++)
            {
                nextPosition = coordinates[i].position;
                
                Gizmos.DrawLine(currentPosition, nextPosition);

                currentPosition = nextPosition;
            }
        }
    }
}