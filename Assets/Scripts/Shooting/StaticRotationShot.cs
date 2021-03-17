using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Shooting
{
    public class StaticRotationShot : Shot
    {
        [Header("Fire Static Rotation")]
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float fireDelay;
        [SerializeField] private int fireCount;
        [SerializeField] private float rotationSpeed = 8f;

        private float _rotationAngle = 0;

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

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + _rotationAngle;
            _rotationAngle += rotationSpeed;
        }
    }
}
