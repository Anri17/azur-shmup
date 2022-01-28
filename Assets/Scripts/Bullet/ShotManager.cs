using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    public class ShotManager : MonoBehaviour
    {
        public static ShotManager Instance { get; private set; }

        private BulletManager _bulletManager;

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;

            _bulletManager = BulletManager.Instance;
        }


        /* -------------------- BULLET COROUTINES -------------------- */

        public IEnumerator LinearBulletCoroutine(Bullet bullet)
        {
            while (true)
            {
                bullet.gameObject.transform.Translate(bullet.gameObject.transform.up * Time.deltaTime * bullet.Speed);
                yield return null;
            }
        }


        /* -------------------- SHOT COROUTINES -------------------- */


        public IEnumerator LinearPatternCoroutine(BulletSettings bulletSettings, float startDelay, float loopDelay,
            float bulletCount)
        {
            yield return new WaitForSeconds(startDelay);

            if (bulletCount <= 0)
            {
                while (true)
                {
                    Bullet bullet = _bulletManager.BulletPool_Get_Bullet(bulletSettings.bulletSprite);
                    
                    bullet.SetupBullet(bulletSettings.bulletSpawnPosition.GetPosition(),
                                       bulletSettings.bulletVelocity.GetSpeed(),
                                       bulletSettings.bulletTarget.GetAngle(),
                                       LinearBulletCoroutine);
                    
                    yield return new WaitForSeconds(loopDelay);
                }
            }

            for (int i = 0; i < bulletCount; i++)
            {
                Bullet bullet = _bulletManager.BulletPool_Get_Bullet(bulletSettings.bulletSprite);
                
                bullet.SetupBullet(bulletSettings.bulletSpawnPosition.GetPosition(),
                                   bulletSettings.bulletVelocity.GetSpeed(),
                                   bulletSettings.bulletTarget.GetAngle(),
                                   LinearBulletCoroutine);

                yield return new WaitForSeconds(loopDelay);
            }

            yield return null;
        }

        /* TODO: Cone Coroutine
        public IEnumerator ConeShotCoroutine(BulletSettings bulletSettings, float startDelay, float loopDelay,
            float bulletCount)
        {
            yield return new WaitForSeconds(startDelay);

            if (bulletCount <= 0)
            {
                while (true)
                {
                    Bullet bullet = _bulletManager.GetBulletFromPool(bulletSettings.bulletType);
                    
                    bullet.SetupBullet(bulletSettings.bulletSpawnPosition.GetPosition(),
                                       bulletSettings.bulletVelocity.GetSpeed(),
                                       bulletSettings.bulletTarget.GetAngle(),
                                       StartCoroutine((LinearBulletCoroutine(bullet))));
                }
            }
        }
        */
    }
}