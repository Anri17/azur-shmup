using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject.Shooting
{
    public class RandomShot : Shot
    {
        [Header("Random Shot")]
        public float startDelay = 1f;
        public float fireDelay;
        public int fireCount;

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + GetRandomAngle();
        }

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            if (fireCount > 0)
            {
                int initialBullet = 0;
                while (initialBullet < fireCount)
                {
                    Fire();
                    yield return new WaitForSeconds(fireDelay);
                    initialBullet++;
                }
                StopCoroutine(FireCoroutine);
            }
            else
            {
                while (true)
                {
                    Fire();
                    yield return new WaitForSeconds(fireDelay);
                }
            }
        }

        private float GetRandomAngle()
        {
            return Random.Range(-180, 180);
        }
    }
}
