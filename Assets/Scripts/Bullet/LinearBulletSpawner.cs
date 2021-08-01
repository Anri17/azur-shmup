using System.Collections;
using System.Collections.Generic;
using AzurProject.Bullet;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class LinearBulletSpawner : BulletSpawner
    {
        public float startDelay;
        public float loopDelay;
        public float bulletCount;
        
        protected override IEnumerator FireCoroutine()
        {
            yield return new WaitForSeconds(startDelay);
            for (int i = 0; i < bulletCount; i++)
            {
                SpawnBullet();
                yield return new WaitForSeconds(loopDelay);
            }
        }
    }
}
