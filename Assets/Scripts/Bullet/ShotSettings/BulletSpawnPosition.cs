using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    public enum BulletSpawnPositionType
    {
        NONE,
        RANDOM,
        SPECIFIC
    }

    public class BulletSpawnPosition : MonoBehaviour
    {
        public float spawnDelay;
        public BulletSpawnPositionType type;
        public Vector2 minPos;
        public Vector2 maxPos;
        public Vector2 specificShotPosition;

        public Vector2 GetPosition()
        {
            switch (type)
            {
                case BulletSpawnPositionType.RANDOM:
                    float x = UnityEngine.Random.Range(minPos.x, maxPos.x);
                    float y = UnityEngine.Random.Range(minPos.y, maxPos.y);

                    return new Vector2(x, y);
                case BulletSpawnPositionType.SPECIFIC:
                    return specificShotPosition;
                default:
                    return transform.position;
            }
        }
    }
}