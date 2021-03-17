using UnityEngine;

namespace AzurProject
{
    public static class AzurProjectBase
    {
        /// <summary>
        /// Find the angle between transform and target
        /// </summary>
        /// <param name="transform">The first transform</param>
        /// <param name="target">The second transform</param>
        /// <returns>angle</returns>
        public static float GetAngle(Transform transform, Transform target)
        {
            Vector3 dir = target.position - transform.position;
            dir = target.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;

            return angle;
        }

        public static Vector3 GetDirection(Transform transform, Transform target)
        {
            Vector3 dir = target.position - transform.position;
            dir = target.InverseTransformDirection(dir);

            return dir;
        }
    }
}
