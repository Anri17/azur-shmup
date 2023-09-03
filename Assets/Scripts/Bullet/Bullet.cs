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

        public BulletGraphicType graphic;

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
        /// <param name="bulletBehaviour">How the bullet will behave during it's life time</param>
        public void StartBullet(BulletSpawnPosition bulletSpawnPosition, BulletBehaviour bulletBehaviour)
        {
            // Spawn Position
            transform.position = bulletSpawnPosition.position + bulletSpawnPosition.offset;

            // Behaviour
            switch (bulletBehaviour.type)
            {
                case BulletBehaviourType.BASIC_A:
                {
                        behaviourCoroutine = StartCoroutine(ShotManager.Instance.BulletBehaviourBasicACoroutine(this, bulletBehaviour.basicA.angle, bulletBehaviour.basicA.speed));
                } break;
                case BulletBehaviourType.BASIC_B:
                    {
                        behaviourCoroutine = StartCoroutine(ShotManager.Instance.BulletBehaviourBasicBCoroutine(this, bulletBehaviour.basicB.speed));
                } break;
            }
        }
    }
}
