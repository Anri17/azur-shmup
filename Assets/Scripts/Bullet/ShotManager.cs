using System.Collections;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.Serialization;
using AzurShmup.Bullet;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace AzurShmup.Stage
{
    public class ShotManager : Singleton<ShotManager>
    {
        [SerializeField] private BulletPool _bulletPool;

        private void Awake()
        {
            MakeSingleton();
        }

        public Coroutine CreateShot(
            ShotData shotData)
        {
            switch (shotData.shotPattern.type)
            {
                case ShotPatternType.LINEAR:
                    {
                        return StartCoroutine(ShotPatternLinearCoroutine(shotData));
                    }
            }

            throw new Exception("Shot pattern not defined.");
        }

        // Remove bullet from Play
        public void Add_Bullet_To_Idle_Pool(Bullet.Bullet bullet) => _bulletPool.Add_Bullet_To_Idle_Pool(bullet);

        private Bullet.Bullet InstantiateBullet(
            BulletGraphicType bulletGraphicType,
            BulletSpawnPosition bulletSpawnPosition,
            BulletBehaviour bulletBehaviour,
            float bulletSpawnDelay)
        {
            Bullet.Bullet bullet = _bulletPool.Get_Bullet(bulletGraphicType);

            bullet.StartBullet(bulletSpawnPosition, bulletBehaviour);

            return bullet;
        }

        /* -------------------- SHOT COROUTINES -------------------- */
        public IEnumerator ShotPatternLinearCoroutine(
            ShotData shotData)
        {
            do
            {
                yield return new WaitForSeconds(shotData.shotPattern.linear.startDelay);

                if (shotData.shotPattern.linear.bulletCount <= 0)
                {
                    while (true)
                    {
                        Bullet.Bullet bullet = InstantiateBullet(
                            shotData.bulletGraphicType,
                            shotData.bulletSpawnPosition,
                            shotData.bulletBehaviour,
                            shotData.bulletSpawnDelay);

                        yield return new WaitForSeconds(shotData.shotPattern.linear.loopDelay);
                    }
                }
                for (int i = 0; i < shotData.shotPattern.linear.bulletCount; i++)
                {
                    Bullet.Bullet bullet = InstantiateBullet(
                        shotData.bulletGraphicType,
                        shotData.bulletSpawnPosition,
                        shotData.bulletBehaviour,
                        shotData.bulletSpawnDelay);

                    yield return new WaitForSeconds(shotData.shotPattern.linear.loopDelay);
                }

                yield return new WaitForSeconds(shotData.loopDelay);
            } while (shotData.loopShot);
            yield return null;
        }

        /* ------------------- BULLET COROUTINES -------------------- */

        public IEnumerator BulletBehaviourBasicACoroutine(Bullet.Bullet bullet, float angle, float speed)
        {
            while (true)
            {
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.transform.Translate(bullet.transform.up * speed * Time.deltaTime, Space.World);
                yield return null;
            }
        }

        public IEnumerator BulletBehaviourBasicBCoroutine(Bullet.Bullet bullet, Vector2 direction)
        {
            Vector2 targetPos;
            float angle = 0;
            while (true)
            {
                targetPos = (Vector2)bullet.transform.position + direction;
                angle = AzurShmupUtilities.GetAngleVector2(bullet.transform.position, targetPos) - 90;
                bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                bullet.transform.Translate(direction * Time.deltaTime, Space.World);
                yield return null;
            }
        }
    }
}