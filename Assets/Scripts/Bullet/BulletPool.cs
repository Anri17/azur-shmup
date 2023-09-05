using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using AzurShmup.Bullet;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

namespace AzurShmup.Bullet
{
    public class BulletPool : MonoBehaviour
    {
        // All the bullets that need to be pooled.
        [SerializeField] private GameObject[] Bullet_Prefabs;

        private Dictionary<BulletGraphic, List<Bullet>> idle_pool;
        private Dictionary<BulletGraphic, List<Bullet>> active_pool;
        private const int BULLETS_POOL_START_SIZE = 12; // this number will be tweaked multiple times through out development

        private void Start()
        {
            idle_pool = new Dictionary<BulletGraphic, List<Bullet>>();
            active_pool = new Dictionary<BulletGraphic, List<Bullet>>();
            
            foreach (GameObject prefab in Bullet_Prefabs)
            {
                Bullet prefab_bullet = prefab.GetComponent<Bullet>();
                
                idle_pool[prefab_bullet.graphic] = new List<Bullet>();
                active_pool[prefab_bullet.graphic] = new List<Bullet>();
                
                for (int i = 0; i < BULLETS_POOL_START_SIZE; i++)
                {
                    Bullet bullet = Instantiate(prefab).GetComponent<Bullet>();
                    idle_pool[bullet.graphic].Add(bullet);
                    bullet.gameObject.SetActive(false);
                }
            }
        }

        /// <summary>Gets a bullet of the given type from the idle pool and sets it to the active pool.</summary>
        /// <param name="type">The type of bullet to be picked from the pool and returned</param>
        /// <returns>Active bullet of the given type.</returns>
        /// <exception cref="Exception">Invalid bullet type</exception>
        public Bullet Get_Bullet(BulletGraphic type)
        {
            // Find a bullet of the given type from the idle pool, set it to the active pool, and return it.
            foreach (Bullet bullet in idle_pool[type])
            {
                idle_pool[type].Remove(bullet);
                active_pool[type].Add(bullet);
                bullet.gameObject.SetActive(true);
                return bullet;
            }
            
            // This code only executes if the idle pool is empty.
            // Create a new bullet of the given type and return it.
            if (idle_pool[type].Count == 0)
            {
                UnityEngine.Debug.Log("BulletPool -> Idle Pool is empty. Creating new Bullet...");
                
                foreach (GameObject bulletPrefab in Bullet_Prefabs)
                {
                    Bullet bulletPrefabScript = bulletPrefab.GetComponent<Bullet>();
                    if (bulletPrefabScript.graphic == type)
                    {
                        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                        active_pool[type].Add(bullet);
                        bullet.gameObject.SetActive(true);
                        return bullet;
                    }
                }
            }

            throw new Exception("Unable to get bullet from Pool.");
        }

        /// <summary>
        /// Removes the given bullet from the active pool and adds it to the idle pool.
        /// </summary>
        /// <param name="bullet">Active bullet to be added to the pool</param>
        public void Add_Bullet_To_Idle_Pool(Bullet bullet)
        {
            if (active_pool[bullet.graphic].Contains(bullet))
            {
                bullet.gameObject.SetActive(false);
                if (active_pool[bullet.graphic].Contains(bullet)) active_pool[bullet.graphic].Remove(bullet);
                idle_pool[bullet.graphic].Add(bullet);
            }
        }

        /// <summary>
        /// Removes all of the bullet from the active pool and adds them to the idle pool.
        /// </summary>
        public void Add_All_Active_Bullets_To_Idle_Pool()
        {
            foreach (var activeBullet in active_pool)
            {
                foreach (Bullet bullet in active_pool[activeBullet.Key])
                {
                    Add_Bullet_To_Idle_Pool(bullet);
                }
            }
        }
    }
}
