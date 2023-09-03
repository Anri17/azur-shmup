using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.Bullet
{
    public class BulletData
    {
        [Serializable]
        public struct Position
        {
            public enum PositionType
            {
                SPECIFIC,
                PARENT,
            }
            public PositionType type;
            public Vector2 specificPosition;

            public Vector2 GetPosition(Vector2 parentPos)
            {
                switch (type)
                {
                    case PositionType.SPECIFIC: return specificPosition;
                    case PositionType.PARENT: return parentPos;
                    default: return Vector2.zero;
                }
            }
        }

        public BulletGraphicType graphic;
        public float delay;
        public Position position;
    }
}
