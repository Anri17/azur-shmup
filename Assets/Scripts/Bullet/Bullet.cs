using System;
using System.Collections;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Serialization;
using AzurShmup.Stage;

namespace AzurShmup.Bullet
{
    [RequireComponent(typeof(BulletLifeCycle))]
    public class Bullet : MonoBehaviour
    {
        public Coroutine behaviourCoroutine;

        public BulletGraphic graphic;

        private ShotManager _shotManager;

        private void Awake()
        {
            _shotManager = ShotManager.Instance;
        }

        public void RemoveFromPlayField()
        {
            transform.position = Vector3.zero;
            StopCoroutine(behaviourCoroutine);
            _shotManager.Add_Bullet_To_Idle_Pool(this);
        }

        /// <summary>
        /// Starts a bullet
        /// </summary>
        /// <param name="pos">The starting position of the bullet</param>
        /// <param name="speed">The starting movement sleep of the bullet</param>
        /// <param name="angle">The starting angle of the bullet</param>
        /// <param name="behaviour">How the bullet will behave during it's life time</param>
        public void StartBullet(BulletSpawnPosition spawnPosition, BulletBehaviourBasicA behaviour, float spawnDelay)
        {
            transform.position = spawnPosition.position + spawnPosition.offset;
            behaviourCoroutine = StartCoroutine(ShotManager.Instance.BulletBehaviourBasicACoroutine(this, behaviour));
        }

        public void StartBullet(BulletSpawnPosition pawnPosition, BulletBehaviourBasicB behaviour, float spawnDelay)
        {
            transform.position = pawnPosition.position + pawnPosition.offset;
            behaviourCoroutine = StartCoroutine(ShotManager.Instance.BulletBehaviourBasicBCoroutine(this, behaviour));
        }

        public void StartBullet(BulletSpawnPosition spawnPosition, BulletBehaviourAcceleratingA behaviour, float spawnDelay)
        {
            transform.position = spawnPosition.position + spawnPosition.offset;
            behaviourCoroutine = StartCoroutine(ShotManager.Instance.BulletBehaviourAcceleratingACoroutine(this, behaviour));
        }
    }
}
