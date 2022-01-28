using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using AzurProject.Bullet;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace AzurProject.Bullet
{
    
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }

        [SerializeField] private GameObject[] Bullet_Prefabs;

        public Dictionary<BulletSprite, List<Bullet>> bullet_pool;
        public Dictionary<BulletSprite, List<Bullet>> active_bullets;
        
        private const int BULLETS_POOL_START_SIZE = 1200; // this number will be tweaked multiple times through out development
        
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        private void Start()
        {
            bullet_pool = new Dictionary<BulletSprite, List<Bullet>>();
            active_bullets = new Dictionary<BulletSprite, List<Bullet>>();
            
            foreach (GameObject prefab in Bullet_Prefabs)
            {
                Bullet prefab_bullet = prefab.GetComponent<Bullet>();
                
                bullet_pool[prefab_bullet.type] = new List<Bullet>();
                active_bullets[prefab_bullet.type] = new List<Bullet>();
                
                for (int i = 0; i < BULLETS_POOL_START_SIZE; i++)
                {
                    Bullet bullet = Instantiate(prefab).GetComponent<Bullet>();
                    bullet.IsPooled = true;
                    bullet.gameObject.SetActive(false);
                    bullet_pool[bullet.type].Add(bullet);
                }
            }
        }

        /// <summary>
        /// Takes a bullet from the pool and returns it
        /// </summary>
        /// <param name="type">The type of bullet to be picked from the pool and returned</param>
        /// <returns>An active bullet</returns>
        /// <exception cref="Exception">Invalid bullet type</exception>
        public Bullet BulletPool_Get_Bullet(BulletSprite type)
        {
            foreach (Bullet bullet in bullet_pool[type])
            {
                if (bullet.IsPooled)
                {
                    bullet_pool[type].Remove(bullet);
                    active_bullets[type].Add(bullet);
                    
                    bullet.IsPooled = false;
                    bullet.gameObject.SetActive(true);
                    
                    return bullet;
                }
            }
            
            // Create a new bullet in case the initial pool of bullets is not enough.
            if (bullet_pool[type].Count == 0)
            {
                Debug.Log("Bullet Pool is empty. Creating new Bullet...");
                
                foreach (GameObject bulletPrefab in Bullet_Prefabs)
                {
                    Bullet bulletPrefabScript = bulletPrefab.GetComponent<Bullet>();
                    if (bulletPrefabScript.type == type)
                    {
                        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                        bullet.IsPooled = false;
                        bullet.gameObject.SetActive(true);
                        active_bullets[type].Add(bullet);
                        return bullet;
                    }
                }
            }

            throw new Exception("Unable to get bullet from Pool.");
        }

        /// <summary>
        /// Adds an active bullet to the pool
        /// </summary>
        /// <param name="bullet">Active bullet to be added to the pool</param>
        public void BulletPool_Add_Bullet(Bullet bullet)
        {
            if (!bullet.IsPooled)
            {
                bullet.IsPooled = true;
                bullet.gameObject.SetActive(false);
                if (active_bullets[bullet.type].Contains(bullet)) active_bullets[bullet.type].Remove(bullet);
                bullet_pool[bullet.type].Add(bullet);
            }
        }

        /// <summary>
        /// Adds all active bullets to the pool
        /// </summary>
        public void BulletPool_Add_All_Active_Bullets()
        {
            foreach (var activeBullet in active_bullets)
            {
                foreach (Bullet bullet in active_bullets[activeBullet.Key])
                {
                    BulletPool_Add_Bullet(bullet);
                }
            }
        }
    }
}
