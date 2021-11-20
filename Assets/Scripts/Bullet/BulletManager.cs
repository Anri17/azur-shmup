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
    public enum BulletType
    {
        // PLAYER
        PLAYER_L          =   0,
        PLAYER_LASER      =   1,
        PLAYER_S          =   2,
          
        // CIRCLE_L  
        CIRCLE_L_BLUE     =  20,
        CIRCLE_L_CYAN     =  21,
        CIRCLE_L_LIME     =  22,
        CIRCLE_L_PURPLE   =  23,
        CIRCLE_L_RED      =  24,
        CIRCLE_L_YELLOW   =  25,
          
        // CIRCLE_M  
        CIRCLE_M_BLACK    =  40,
        CIRCLE_M_BLUE     =  41,
        CIRCLE_M_CYAN     =  42,
        CIRCLE_M_GREEN    =  43,
        CIRCLE_M_LIME     =  44,
        CIRCLE_M_NAVY     =  45,
        CIRCLE_M_ORANGE   =  46,
        CIRCLE_M_PYNK     =  47,
        CIRCLE_M_PURPLE   =  48,
        CIRCLE_M_RED      =  49,
        CIRCLE_M_TEAL     =  50,
        CIRCLE_M_VIOLET   =  51,
        CIRCLE_M_WHITE    =  52,
        CIRCLE_M_YELLOW   =  53,
          
        // CIRCLE_S  
        CIRCLE_S_BLACK    =  60,
        CIRCLE_S_BLUE     =  61,
        CIRCLE_S_CYAN     =  62,
        CIRCLE_S_GREEN    =  63,
        CIRCLE_S_LIME     =  64,
        CIRCLE_S_NAVY     =  65,
        CIRCLE_S_ORANGE   =  66,
        CIRCLE_S_PYNK     =  67,
        CIRCLE_S_PURPLE   =  68,
        CIRCLE_S_RED      =  69,
        CIRCLE_S_TEAL     =  70,
        CIRCLE_S_VIOLET   =  71,
        CIRCLE_S_WHITE    =  72,
        CIRCLE_S_YELLOW   =  73,
          
        // RECT_S  
        RECT_S_BLUE       =  80,
        RECT_S_CYAN       =  81,
        RECT_S_GREEN      =  82,
        RECT_S_LIME       =  83,
        RECT_S_NAVY       =  84,
        RECT_S_ORANGE     =  85,
        RECT_S_PYNK       =  86,
        RECT_S_PURPLE     =  87,
        RECT_S_RED        =  88,
        RECT_S_TEAL       =  89,
        RECT_S_VIOLET     =  90,
        RECT_S_YELLOW     =  91,
          
        // RICE_S  
        RICE_S_BLUE       = 100,
        RICE_S_CYAN       = 101,
        RICE_S_GREEN      = 102,
        RICE_S_LIME       = 103,
        RICE_S_NAVY       = 104,
        RICE_S_ORANGE     = 105,
        RICE_S_PYNK       = 106,
        RICE_S_PURPLE     = 107,
        RICE_S_RED        = 108,
        RICE_S_TEAL       = 109,
        RICE_S_VIOLET     = 110,
        RICE_S_YELLOW     = 111,
          
        // TEAR_S  
        TEAR_S_BLUE       = 120,
        TEAR_S_CYAN       = 121,
        TEAR_S_GREEN      = 122,
        TEAR_S_LIME       = 123,
        TEAR_S_NAVY       = 124,
        TEAR_S_ORANGE     = 125,
        TEAR_S_PYNK       = 126,
        TEAR_S_PURPLE     = 127,
        TEAR_S_RED        = 128,
        TEAR_S_TEAL       = 129,
        TEAR_S_VIOLET     = 130,
        TEAR_S_YELLOW     = 131
    }
    
    public class BulletManager : MonoBehaviour
    {
        public static BulletManager Instance { get; private set; }

        [Header("Bullet Prefabs")] [SerializeField]
        private GameObject[] Bullet_Prefabs;

        public Dictionary<BulletType, List<Bullet>> pooledBullets;
        public Dictionary<BulletType, List<Bullet>> activeBullets;
        
        private const int AMOUNT_OF_BULLETS_TO_POOL = 10;
        
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        private void Start()
        {
            pooledBullets = new Dictionary<BulletType, List<Bullet>>();
            activeBullets = new Dictionary<BulletType, List<Bullet>>();
            
            foreach (GameObject bulletPrefab in Bullet_Prefabs)
            {
                Bullet prefabBulletType = bulletPrefab.GetComponent<Bullet>();
                
                pooledBullets[prefabBulletType.type] = new List<Bullet>();
                activeBullets[prefabBulletType.type] = new List<Bullet>();
                
                for (int i = 0; i < AMOUNT_OF_BULLETS_TO_POOL; i++)
                {
                    Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                    bullet.IsPooled = true;
                    bullet.gameObject.SetActive(false);
                    pooledBullets[prefabBulletType.type].Add(bullet);
                }
            }
        }

        public Bullet GetBulletFromPool(BulletType bulletType)
        {
            foreach (Bullet pooledBullet in pooledBullets[bulletType])
            {
                if (pooledBullet.IsPooled)
                {
                    pooledBullets[bulletType].Remove(pooledBullet);
                    activeBullets[bulletType].Add(pooledBullet);
                    
                    pooledBullet.IsPooled = false;
                    pooledBullet.gameObject.SetActive(true);
                    
                    return pooledBullet;
                }
            }
            
            // Create a new bullet in case the initial pool of bullets is not enough.
            if (pooledBullets[bulletType].Count == 0)
            {
                Debug.Log("Bullet Pool is empty. Creating new Bullet...");
                
                foreach (GameObject bulletPrefab in Bullet_Prefabs)
                {
                    Bullet bulletPrefabScript = bulletPrefab.GetComponent<Bullet>();
                    if (bulletPrefabScript.type == bulletType)
                    {
                        Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
                        bullet.IsPooled = false;
                        bullet.gameObject.SetActive(true);
                        activeBullets[bulletType].Add(bullet);
                        return bullet;
                    }
                }
            }

            throw new Exception("Unable to get bullet from Pool.");
        }

        public void AddBulletToPool(Bullet bullet)
        {
            if (!bullet.IsPooled)
            {
                bullet.IsPooled = true;
                bullet.gameObject.SetActive(false);
                if (activeBullets[bullet.type].Contains(bullet)) activeBullets[bullet.type].Remove(bullet);
                pooledBullets[bullet.type].Add(bullet);
            }
        }

        public void AddAllActiveBulletsToPool()
        {
            foreach (var activeBullet in activeBullets)
            {
                foreach (Bullet bullet in activeBullets[activeBullet.Key])
                {
                    AddBulletToPool(bullet);
                }
            }
        }
    }
}
