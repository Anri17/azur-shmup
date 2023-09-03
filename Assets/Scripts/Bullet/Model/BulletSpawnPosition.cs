using UnityEngine;

namespace AzurShmup.Bullet
{
    [System.Serializable]
    public class BulletSpawnPosition
    {
        public BulletSpawnPositionType type;

        public Vector2 position;
        public Vector2 offset;
    }
}