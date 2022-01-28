using UnityEngine;

namespace AzurProject.Bullet
{

    public enum BulletSprite
    {
        // PLAYER
        PLAYER_L = 0,
        PLAYER_LASER = 1,
        PLAYER_S = 2,

        // CIRCLE_L  
        CIRCLE_L_BLUE = 20,
        CIRCLE_L_CYAN = 21,
        CIRCLE_L_LIME = 22,
        CIRCLE_L_PURPLE = 23,
        CIRCLE_L_RED = 24,
        CIRCLE_L_YELLOW = 25,

        // CIRCLE_M  
        CIRCLE_M_BLACK = 40,
        CIRCLE_M_BLUE = 41,
        CIRCLE_M_CYAN = 42,
        CIRCLE_M_GREEN = 43,
        CIRCLE_M_LIME = 44,
        CIRCLE_M_NAVY = 45,
        CIRCLE_M_ORANGE = 46,
        CIRCLE_M_PINK = 47,
        CIRCLE_M_PURPLE = 48,
        CIRCLE_M_RED = 49,
        CIRCLE_M_TEAL = 50,
        CIRCLE_M_VIOLET = 51,
        CIRCLE_M_WHITE = 52,
        CIRCLE_M_YELLOW = 53,

        // CIRCLE_S  
        CIRCLE_S_BLACK = 60,
        CIRCLE_S_BLUE = 61,
        CIRCLE_S_CYAN = 62,
        CIRCLE_S_GREEN = 63,
        CIRCLE_S_LIME = 64,
        CIRCLE_S_NAVY = 65,
        CIRCLE_S_ORANGE = 66,
        CIRCLE_S_PINK = 67,
        CIRCLE_S_PURPLE = 68,
        CIRCLE_S_RED = 69,
        CIRCLE_S_TEAL = 70,
        CIRCLE_S_VIOLET = 71,
        CIRCLE_S_WHITE = 72,
        CIRCLE_S_YELLOW = 73,

        // RECT_S  
        RECT_M_BLUE = 80,
        RECT_M_CYAN = 81,
        RECT_M_GREEN = 82,
        RECT_M_LIME = 83,
        RECT_M_NAVY = 84,
        RECT_M_ORANGE = 85,
        RECT_M_PINK = 86,
        RECT_M_PURPLE = 87,
        RECT_M_RED = 88,
        RECT_M_TEAL = 89,
        RECT_M_VIOLET = 90,
        RECT_M_YELLOW = 91,

        // RICE_S  
        RICE_S_BLUE = 100,
        RICE_S_CYAN = 101,
        RICE_S_GREEN = 102,
        RICE_S_LIME = 103,
        RICE_S_NAVY = 104,
        RICE_S_ORANGE = 105,
        RICE_S_PINK = 106,
        RICE_S_PURPLE = 107,
        RICE_S_RED = 108,
        RICE_S_TEAL = 109,
        RICE_S_VIOLET = 110,
        RICE_S_YELLOW = 111,

        // TEAR_S  
        TEAR_S_BLUE = 120,
        TEAR_S_CYAN = 121,
        TEAR_S_GREEN = 122,
        TEAR_S_LIME = 123,
        TEAR_S_NAVY = 124,
        TEAR_S_ORANGE = 125,
        TEAR_S_PINK = 126,
        TEAR_S_PURPLE = 127,
        TEAR_S_RED = 128,
        TEAR_S_TEAL = 129,
        TEAR_S_VIOLET = 130,
        TEAR_S_YELLOW = 131
    }

    public enum BulletType
    {
        LINEAR
    }

    [RequireComponent(typeof(BulletSpawnPosition))]
    [RequireComponent(typeof(BulletTarget))]
    [RequireComponent(typeof(BulletVelocity))]
    public class BulletSettings : MonoBehaviour
    {
        public BulletSprite bulletSprite;
        public BulletType bulletType;

        public BulletSpawnPosition bulletSpawnPosition;
        public BulletTarget bulletTarget;
        public BulletVelocity bulletVelocity;

        private void Reset()
        {
            bulletSpawnPosition = GetComponent<BulletSpawnPosition>();
            bulletTarget = GetComponent<BulletTarget>();
            bulletVelocity = GetComponent<BulletVelocity>();

            if (bulletSpawnPosition == null)
            {
                bulletSpawnPosition = gameObject.AddComponent<BulletSpawnPosition>();
            }
            
            if (bulletTarget == null)
            {
                bulletTarget = gameObject.AddComponent<BulletTarget>();
            }
            
            if (bulletVelocity == null)
            {
                bulletVelocity = gameObject.AddComponent<BulletVelocity>();
            }
        }
    }
}
