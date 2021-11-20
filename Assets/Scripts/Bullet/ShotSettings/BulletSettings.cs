using UnityEngine;

namespace AzurProject.Bullet
{
    [RequireComponent(typeof(BulletSpawnPosition))]
    [RequireComponent(typeof(BulletTarget))]
    [RequireComponent(typeof(BulletVelocity))]
    public class BulletSettings : MonoBehaviour
    {
        public BulletType bulletType;
        
        public BulletSpawnPosition bulletSpawnPosition;
        public BulletTarget bulletTarget;
        public BulletVelocity bulletVelocity;

        private void Reset()
        {
            bulletSpawnPosition = GetComponent<BulletSpawnPosition>();
            bulletTarget = GetComponent<BulletTarget>();
            bulletVelocity = GetComponent<BulletVelocity>();

            if (bulletSpawnPosition == null)
            {
                bulletSpawnPosition = gameObject.AddComponent<BulletSpawnPosition>();
            }
            
            if (bulletTarget == null)
            {
                bulletTarget = gameObject.AddComponent<BulletTarget>();
            }
            
            if (bulletVelocity == null)
            {
                bulletVelocity = gameObject.AddComponent<BulletVelocity>();
            }
        }
    }
}
