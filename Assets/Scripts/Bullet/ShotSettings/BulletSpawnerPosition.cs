using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    public enum ShotPositionType
    {
        NONE,
        RANDOM,
        SPECIFIC
    }
    
    public class BulletSpawnerPosition : MonoBehaviour
    {
        public ShotPositionType shotPositionType;
        public Vector2 minPos;
        public Vector2 maxPos;
        public Vector2 specificShotPosition;

        public Vector2 GetPosition()
        {
            switch (shotPositionType)
            {
                case ShotPositionType.RANDOM:
                    float x = Random.Range(minPos.x, maxPos.x);
                    float y = Random.Range(minPos.y, maxPos.y);

                    return new Vector2(x, y);
                case ShotPositionType.SPECIFIC:
                    return specificShotPosition;
                default:
                    return transform.position;
            }
        }
    }
}
