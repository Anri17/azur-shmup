using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Shooting
{
    public class RingShot : Shot
    {
        [Header("Ring Shot")]
        [SerializeField] private int directions = 8;
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float fireDelay;
        [SerializeField] private int fireCount;

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform);
        }

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            if (fireCount > 0)
            {
                int initialBullet = 0;
                while (initialBullet < fireCount)
                {
                    float currentAngle = 0;
                    while (currentAngle < 360)
                    {
                        Fire();

                        BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform) + currentAngle;
                        currentAngle += GetRingAngle(directions);
                    }

                    yield return new WaitForSeconds(fireDelay);
                    initialBullet++;
                }
                StopCoroutine(FireCoroutine);
            }
            else
            {
                while (true)
                {
                    float currentAngle = 0;
                    while (currentAngle < 360)
                    {
                        Fire();

                        BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform) + currentAngle;
                        currentAngle += GetRingAngle(directions);
                    }

                    yield return new WaitForSeconds(fireDelay);
                }
            }
        }
        
        private float GetRingAngle(int bulletCount)
        {
            if (bulletCount <= 0)
            {
                bulletCount = 1;
            }

            return 360.0f / (float)bulletCount;
        }
    }
}
