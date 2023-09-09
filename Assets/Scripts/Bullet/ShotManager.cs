using System.Collections;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.Serialization;
using AzurShmup.Bullet;
using System;
using System.Runtime.InteropServices.ComTypes;
using AzurShmup.Core;
using UnityEditor.Graphs;

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
                case ShotType.CIRCULAR_BASIC_A:
                    return StartCoroutine(ShotCoroutine_CircularBasicA(shotData.shot.circularBasicA));
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
                    if (shot.targetPlayer)
                    {
                        shot.bulletBehaviour.angle = AzurShmupUtilities.GetAngleVector2(shot.bulletSpawnPosition.position, GameManager.Instance.Player.transform.position) + shot.angleOffset - 90.0f;
                    }
                    
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
                    if (shot.targetPlayer)
                    {
                        shot.bulletBehaviour.angle = AzurShmupUtilities.GetAngleVector2(shot.bulletSpawnPosition.position, GameManager.Instance.Player.transform.position) + shot.angleOffset - 90.0f;
                    }

                    Bullet.Bullet bullet = _bulletPool.Get_Bullet(shot.bulletGraphic);
                    bullet.StartBullet(shot.bulletSpawnPosition, shot.bulletBehaviour, shot.bulletSpawnDelay);

                    if (!shot.is_infinite_shots) ++i;
                    yield return new WaitForSeconds(shot.shoot_delay);
                }

                yield return new WaitForSeconds(shot.loop_delay);
            } while (shot.loop_shot);
            yield return null;
        }

        
        public IEnumerator ShotCoroutine_CircularBasicA(ShotCircularBasicA shot)
        {
            // Linear

            yield return new WaitForSeconds(shot.start_delay);
            do
            {
                int i = 0;
                float delta_angle = shot.end_angle - shot.start_angle;
                float offset_angle = delta_angle / (shot.shot_directions-1);
                float angle = shot.start_angle;
                while (i < shot.shoot_count || shot.is_infinite_shots)
                {
                    for (int j = 0; j < shot.shot_size; ++j)
                    {
                        shot.bulletBehaviour.angle = angle;
                        Bullet.Bullet bullet = _bulletPool.Get_Bullet(shot.bulletGraphic);
                        bullet.StartBullet(shot.bulletSpawnPosition, shot.bulletBehaviour, shot.bulletSpawnDelay);
                        if (shot.is_random_directions)
                        {
                            angle = UnityEngine.Random.Range(shot.start_angle, shot.end_angle);
                        }
                        else
                        {
                            angle += offset_angle;
                            // define a 0.1f margin to account for floating point rounding errors
                            // (so that if angle = 199.99999, its value doesn't get reset if the min/max is 200, as an example)
                            if (delta_angle > 0 && angle > shot.end_angle+0.1f)
                                angle = shot.start_angle;
                            else if (delta_angle < 0 && angle < shot.end_angle-0.1f)
                                angle = shot.start_angle;
                        }
                    }

                    if (!shot.is_infinite_shots) ++i;
                    yield return new WaitForSeconds(shot.shoot_delay);
                }

                yield return new WaitForSeconds(shot.loop_delay);
            } while (shot.loop_shot);
            yield return null;
        }
        

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