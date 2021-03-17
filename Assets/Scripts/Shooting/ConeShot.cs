using System;
using System.Collections;
using UnityEngine;

namespace AzurProject.Shooting
{
    public class ConeShot : Shot
    {
        [Header("Cone Shot")]
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private int directions = 8;
        [SerializeField] private float directionDelay;
        [SerializeField] private float fireDelay;
        [SerializeField] private int fireCount;
        [Header("Barrage Delay")]
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float barrageDelay; 

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
        }

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            if (fireCount > 0)
            {
                do
                {
                    int initialBullet = 0;
                    while (initialBullet < fireCount)
                    {
                        float currentAngle = minAngle;
                        float angleDifference = GetRingAngle(directions);
                        int directionIndex = 0;
                        while (directionIndex <= Math.Abs(directions))
                        {
                            Fire();

                            if (target != null)
                            {
                                BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform,
                                    GameObject.FindGameObjectWithTag(target.tag).transform) + currentAngle;
                            }
                            else
                            {
                                BulletScript.Angle = aimOffset + currentAngle;
                            }

                            currentAngle += angleDifference;

                            directionIndex++;
                            if (directionDelay > 0)
                            {
                                yield return new WaitForSeconds(directionDelay);
                            }
                        }

                        yield return new WaitForSeconds(fireDelay);
                        initialBullet++;
                    }

                    yield return new WaitForSeconds(barrageDelay);
                } while (barrageDelay > 0);
                StopCoroutine(FireCoroutine);
            }
            else
            {
                while (true)
                {
                    float currentAngle = minAngle;
                    float angleDifference = GetRingAngle(directions);
                    int directionIndex = 0;
                    while (directionIndex <= Math.Abs(directions))
                    {
                        Fire();

                        if (target != null)
                        {
                            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform) + currentAngle;
                        }
                        else
                        {
                            BulletScript.Angle = aimOffset + currentAngle;
                        }

                        currentAngle += angleDifference;

                        directionIndex++;
                        if (directionDelay > 0)
                        {
                            yield return new WaitForSeconds(directionDelay);
                        }
                    }

                    yield return new WaitForSeconds(fireDelay);
                }
            }
        }
        
        private float GetRingAngle(int bulletCount)
        {
            if (bulletCount == 0)
            {
                bulletCount = 1;
            }

            return (maxAngle - minAngle) / bulletCount;
        }
    }
}
