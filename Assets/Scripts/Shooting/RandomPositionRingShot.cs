using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using AzurProject.Core;
using Random = UnityEngine.Random;

namespace AzurProject.Shooting
{
    public class RandomPositionRingShot : Shot
    {
        [Header("Fire Ring")]
        [SerializeField] private int directions = 8;
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float fireDelay;
        [SerializeField] private int fireCount;

        private Vector3 _randomPosition;

        private GameObject _targetToFireTo;

        private void Start()
        {
            _targetToFireTo = GameObject.FindWithTag(target.tag);
        }

        protected override void Fire()
        {
            BulletGameObject = Instantiate(bullet, _randomPosition, Quaternion.identity);
            BulletScript = BulletGameObject.GetComponent<Bullet.Bullet>();

            BulletScript.Speed = speed;
            BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, _targetToFireTo.transform);
        }

        protected override IEnumerator ShootCoroutine()
        {
            yield return new WaitForSeconds(startDelay);

            if (fireCount > 0)
            {
                int initialBullet = 0;
                _randomPosition = GetRandomPoint();
                while (initialBullet < fireCount)
                {
                    float currentAngle = 0;
                    float ringAngle = GetRingAngle(directions);
                    while (currentAngle < 360)
                    {
                        Fire();

                        BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, _targetToFireTo.transform) + currentAngle;
                        currentAngle += ringAngle;
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
                    float ringAngle = GetRingAngle(directions);
                    _randomPosition = GetRandomPoint();
                    while (currentAngle < 360)
                    {
                        Fire();

                        BulletScript.Angle = aimOffset + AzurProjectBase.GetAngle(transform, _targetToFireTo.transform) + currentAngle;
                        currentAngle += ringAngle;
                    }

                    yield return new WaitForSeconds(fireDelay);
                }
            }
        }

        private int GetRingAngle(int shotCount)
        {
            if (shotCount <= 0)
            {
                shotCount = 1;
            }

            return 360 / shotCount;
        }
        
        private Vector3 GetRandomPoint()
        {
            float randX = Random.Range((GameManager.GAME_FIELD_TOP_LEFT.x) + GameManager.GAME_FIELD_CENTER.x, (GameManager.GAME_FIELD_TOP_RIGHT.x) + GameManager.GAME_FIELD_CENTER.x);
            float randY = Random.Range(1, (GameManager.GAME_FIELD_TOP_LEFT.y));

            return new Vector3(randX, randY, 0);
        }
    }
}
