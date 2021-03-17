using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AzurProject.Core;

namespace AzurProject.Shooting
{
    public class RandomTimedShot : Shot
    {
        [Header("Fire Random Time")]
        [SerializeField] private float minTimeDelay = 1f;
        [SerializeField] private float maxTimeDelay = 1f;
        [SerializeField] private int fireCount;

        private float _fireDelay;

        private void Start()
        {
            _fireDelay = Random.Range(minTimeDelay, maxTimeDelay);
        }

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, GameObject.FindGameObjectWithTag(target.tag).transform);
        }

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(_fireDelay);

            if (fireCount > 0)
            {
                int bulletIndex = 0;
                while (bulletIndex < fireCount)
                {
                    Fire();
                    yield return new WaitForSeconds(_fireDelay);
                    bulletIndex++;
                }
                StopCoroutine(FireCoroutine);
            }
            else
            {
                while (true)
                {
                    Fire();
                    yield return new WaitForSeconds(_fireDelay);
                }
            }
        }
    }
}
