using System;
using System.Collections;
using UnityEngine;

namespace AzurProject.Movement
{
    [Serializable]
    public class BezierRoute
    {
        public Vector3 point0;
        public Vector3 point1;
        public Vector3 point2;
        public Vector3 point3;

        public float duration = 1.0f;
        public float waitTime;
    }
    
    [Serializable]
    public class BezierMovement : Movement
    {
        public BezierRoute[] bezierRoutes;
        
        [SerializeField] private float delayTime;
        [SerializeField] private bool loopMovement;

        public override IEnumerator MoveCoroutine()
        {
            yield return new WaitForSeconds(delayTime);
            
            do
            {
                for (int routeIndex = 0; routeIndex < bezierRoutes.Length; routeIndex++)
                {
                    float tParam = 0f;
                    Vector3 p0 = bezierRoutes[routeIndex].point0;
                    Vector3 p1 = bezierRoutes[routeIndex].point1;
                    Vector3 p2 = bezierRoutes[routeIndex].point2;
                    Vector3 p3 = bezierRoutes[routeIndex].point3;

                    while (tParam < 1)
                    {
                        tParam += Time.deltaTime * bezierRoutes[routeIndex].duration;

                        MainTransform.position = Bezier.CalculateCubicPoint(tParam, p0, p1, p2, p3);

                        yield return new WaitForEndOfFrame();
                    }

                    yield return new WaitForSeconds(bezierRoutes[routeIndex].waitTime);
                }
            } while (loopMovement);
        }

        private Transform[] GetRoutesFromPath(Transform path)
        {
            Transform[] routes = new Transform[path.childCount];
            for (int i = 0; i < path.childCount; i++)
            {
                routes[i] = path.GetChild(i);
            }

            return routes;
        }
        
        private void OnDrawGizmosSelected()
        {
            if (bezierRoutes == null)
            {
                return;
            }
            for (var index = 0; index < bezierRoutes.Length; index++)
            {
                var bezierRoute = bezierRoutes[index];
                Vector3 previousPoint = bezierRoute.point0;
                Vector3 nextPoint;

                for (float t = 0; t <= 1; t += 0.03125f)
                {
                    nextPoint = Bezier.CalculateCubicPoint(t, bezierRoute.point0, bezierRoute.point1,
                        bezierRoute.point2, bezierRoute.point3);

                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(previousPoint, nextPoint);

                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(nextPoint, 0.125f);

                    previousPoint = nextPoint;
                }

                Gizmos.color = Color.white;
                Gizmos.DrawLine(bezierRoute.point0, bezierRoute.point1);
                Gizmos.DrawLine(bezierRoute.point2, bezierRoute.point3);
            }
        }
    }
}