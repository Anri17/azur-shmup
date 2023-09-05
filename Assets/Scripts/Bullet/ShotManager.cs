using System.Collections;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.Serialization;
using AzurShmup.Bullet;
using System;
using System.Runtime.InteropServices.ComTypes;
using AzurShmup.Core;

namespace AzurShmup.Stage
{
    public class ShotManager : Singleton<ShotManager>
    {
        [SerializeField] private BulletPool _bulletPool;

        private void Awake()
        {
            MakeSingleton();
        }

        public Coroutine StartShot(
            ShotData shotData)
        {
            switch (shotData.shot.type)
            {
                case ShotType.LINEAR_BASIC_A:
                    return StartCoroutine(ShotCoroutine_LinearBasicA(shotData.shot.linearBasicA));
                case ShotType.LINEAR_BASIC_B:
                    return StartCoroutine(ShotCoroutine_LinearBasicB(shotData.shot.linearBasicB));
                case ShotType.LINEAR_ACCELERATING_A:
                    return StartCoroutine(ShotCoroutine_LinearAcceleratingA(shotData.shot.linearAcceleratingA));
            }

            throw new Exception("Shot pattern not defined.");
        }

        // Remove bullet from Play
        public void Add_Bullet_To_Idle_Pool(Bullet.Bullet bullet) => _bulletPool.Add_Bullet_To_Idle_Pool(bullet);

        /* -------------------- SHOT COROUTINES -------------------- */
        public IEnumerator ShotCoroutine_LinearBasicA(ShotLinearBasicA shot)
        {
            yield return new WaitForSeconds(shot.start_delay);
            do
            {
                int i = 0;
                while (i < shot.shoot_count || shot.is_infinite_shots)
                {
                    Bullet.Bullet bullet = _bulletPool.Get_Bullet(shot.bulletGraphic);
                    bullet.StartBullet(shot.bulletSpawnPosition, shot.bulletBehaviour, shot.bulletSpawnDelay);

                    if (!shot.is_infinite_shots) ++i;
                    yield return new WaitForSeconds(shot.shoot_delay);
                }

                yield return new WaitForSeconds(shot.loop_delay);
            } while (shot.loop_shot);
            yield return null;
        }
        public IEnumerator ShotCoroutine_LinearBasicB(ShotLinearBasicB shot)
        {
            yield return new WaitForSeconds(shot.start_delay);
            do
            {
                int i = 0;
                while (i < shot.shoot_count || shot.is_infinite_shots)
                {
                    Bullet.Bullet bullet = _bulletPool.Get_Bullet(shot.bulletGraphic);
                    bullet.StartBullet(shot.bulletSpawnPosition, shot.bulletBehaviour, shot.bulletSpawnDelay);

                    if (!shot.is_infinite_shots) ++i;
                    yield return new WaitForSeconds(shot.shoot_delay);
                }

                yield return new WaitForSeconds(shot.loop_delay);
            } while (shot.loop_shot);
            yield return null;
        }
        public IEnumerator ShotCoroutine_LinearAcceleratingA(ShotLinearAcceleratingA shot)
        {
            yield return new WaitForSeconds(shot.start_delay);
            do
            {
                int i = 0;
                while (i < shot.shoot_count || shot.is_infinite_shots)
                {
                    Bullet.Bullet bullet = _bulletPool.Get_Bullet(shot.bulletGraphic);
                    bullet.StartBullet(shot.bulletSpawnPosition, shot.bulletBehaviour, shot.bulletSpawnDelay);

                    if (!shot.is_infinite_shots) ++i;
                    yield return new WaitForSeconds(shot.shoot_delay);
                }

                yield return new WaitForSeconds(shot.loop_delay);
            } while (shot.loop_shot);
            yield return null;
        }

        /*
        public IEnumerator ShotCoroutine_Rotating(ShotData shotData)
        {
            do
            {
                yield return new WaitForSeconds(shotData.shot.rotating.shoot_delay);

                if (shotData.shot.rotating.cluster_count <= 0)
                {
                    while (true)
                    {
                        // set bullet starting angles
                        if (shotData.shot.rotating.isRandom)
                        {

                        }
                        for (int i = 0; i < shotData.shot.rotating.cluster_size; ++i)
                        {
                            Bullet.Bullet bullet = InstantiateBullet(
                                shotData.bulletGraphicType,
                                shotData.bulletSpawnPosition,
                                shotData.bulletBehaviour,
                                shotData.bulletSpawnDelay);
                        }

                        yield return new WaitForSeconds(shotData.shot.rotating.shoot_delay);
                    }
                }
                for (int i = 0; i < shotData.shot.rotating.cluster_count; i++)
                {
                    for (int j = 0; j < shotData.shot.rotating.cluster_size; ++j)
                    {
                        Bullet.Bullet bullet = InstantiateBullet(
                            shotData.bulletGraphicType,
                            shotData.bulletSpawnPosition,
                            shotData.bulletBehaviour,
                            shotData.bulletSpawnDelay);

                        float angle = 0;
                    }

                    yield return new WaitForSeconds(shotData.shot.rotating.shoot_delay);
                }

                yield return new WaitForSeconds(shotData.loopDelay);
            } while (shotData.loopShot);
            yield return null;
        }
        */

        /* ------------------- BULLET COROUTINES -------------------- */

        public IEnumerator BulletBehaviourBasicACoroutine(Bullet.Bullet bullet, BulletBehaviourBasicA bulletBehaviourBasicA)
        {
            while (true)
            {
                if (!GameManager.Instance.GamePaused)
                {
                    bullet.transform.rotation = Quaternion.Euler(0, 0, bulletBehaviourBasicA.angle);
                    bullet.transform.Translate(bullet.transform.up * bulletBehaviourBasicA.speed * Time.deltaTime, Space.World);
                }
                yield return null;
            }
        }

        public IEnumerator BulletBehaviourBasicBCoroutine(Bullet.Bullet bullet, BulletBehaviourBasicB bulletBehaviourBasicB)
        {
            Vector2 targetPos;
            float angle = 0;
            while (true)
            {
                if (!GameManager.Instance.GamePaused)
                {
                    targetPos = (Vector2)bullet.transform.position + bulletBehaviourBasicB.speed;
                    angle = AzurShmupUtilities.GetAngleVector2(bullet.transform.position, targetPos) - 90;
                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                    bullet.transform.Translate(bulletBehaviourBasicB.speed * Time.deltaTime, Space.World);
                }
                yield return null;
            }
        }

        public IEnumerator BulletBehaviourAcceleratingACoroutine(Bullet.Bullet bullet, BulletBehaviourAcceleratingA bulletBehaviourAcceleratingA)
        {
            float angle = bulletBehaviourAcceleratingA.angle;
            float speed = bulletBehaviourAcceleratingA.speed;
            while (true)
            {
                if (!GameManager.Instance.GamePaused)
                {
                    bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
                    bullet.transform.Translate(bullet.transform.up * speed * Time.deltaTime, Space.World);
                    angle += bulletBehaviourAcceleratingA.angle_change;
                    speed += bulletBehaviourAcceleratingA.speed_change;
                    speed = speed > bulletBehaviourAcceleratingA.speed_max ? bulletBehaviourAcceleratingA.speed_max : speed;
                }
                yield return null;  
            }
        }
    }
}