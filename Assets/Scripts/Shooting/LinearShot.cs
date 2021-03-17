using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Shooting
{
    public class LinearShot : Shot
    {
        [Header("Static Shot")]
        public float startDelay = 1f;
        public float fireDelay;
        public int fireCount;

        [Header("Shot Iteration")]
        public float shotDelay;

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            do
            {
                if (fireCount > 0)
                {
                    int bulletIndex = 0;
                    while (bulletIndex < fireCount)
                    {
                        Fire();
                        yield return new WaitForSeconds(fireDelay);
                        bulletIndex++;
                    }
                    // StopCoroutine(FireCoroutine);
                }
                else
                {
                    while (true)
                    {
                        Fire();
                        yield return new WaitForSeconds(fireDelay);
                    }
                }
                
                yield return new WaitForSeconds(shotDelay);
            } while (shotDelay > 0);
        }

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform);
        }
    }
}
