using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    public enum BulletVelocityType
    {
        SIMPLE
    }
    
    public class BulletVelocity : MonoBehaviour
    {
        public BulletVelocityType type;
        
        public float speed;

        public float GetSpeed()
        {
            return speed;
        }
    }
}
