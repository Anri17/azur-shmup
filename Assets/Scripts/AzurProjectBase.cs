using System.IO;
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
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            return angle;
        }
        
        public static float GetAngle(Vector3 position, Transform target)
        {
            Vector3 dir = target.position - position;
            dir = target.InverseTransformDirection(dir);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

            return angle;
        }

        public static Vector3 GetDirection(Transform transform, Transform target)
        {
            Vector3 dir = target.position - transform.position;
            dir = target.InverseTransformDirection(dir);

            return dir;
        }
        
        public static Sprite LoadSprite(string filePath, float pixelPerUnit)
        {
            Texture2D tex2D = new Texture2D(2, 2);

            byte[] imageData = File.ReadAllBytes(filePath);
            string[] path = filePath.Split('\\');
            
            tex2D.LoadImage(imageData);
            tex2D.filterMode = FilterMode.Point;
            tex2D.Compress(false);
            
            Rect rect = new Rect(Vector2.zero, new Vector2(tex2D.width, tex2D.height));
            Sprite sprite = Sprite.Create(tex2D, rect, Vector2.zero, pixelPerUnit);

            sprite.name = path[path.Length - 1]; // name of the texture is the last of the path
            
            return sprite;
        }
    }
}
