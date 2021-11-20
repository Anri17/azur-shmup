using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class PlayerBullet : Bullet
    {
        [SerializeField] protected float damage = 1f;
        
        public float Damage { get => damage; set => damage = value; }
    }
}
