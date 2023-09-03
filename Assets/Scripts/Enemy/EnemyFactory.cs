using AzurShmup.Data;
using UnityEngine;

namespace AzurShmup.Enemy
{
    public static class EnemyFactory
    {
        public static GameObject BuildNormalEnemy(NormalEnemyData normalEnemyData)
        {
            GameObject enemyGameObject = new GameObject();
            
            // base
            enemyGameObject.name = normalEnemyData.Name;
            enemyGameObject.tag = "Enemy";
            enemyGameObject.layer = 8; // layer 8 = "Enemy"
            enemyGameObject.transform.position = normalEnemyData.Position;

            // sprite
            SpriteRenderer spriteRenderer = enemyGameObject.AddComponent<SpriteRenderer>();
            Sprite sprite = AzurShmupUtilities.LoadSprite(normalEnemyData.Sprite, 21.333333f);
            spriteRenderer.sprite = sprite;
            spriteRenderer.sortingLayerName = "Enemy";
            
            // rigidbody
            Rigidbody2D rigidbody2D = enemyGameObject.AddComponent<Rigidbody2D>();
            rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            
            // collider
            BoxCollider2D boxCollider2D = enemyGameObject.AddComponent<BoxCollider2D>();
            boxCollider2D.isTrigger = true;
            boxCollider2D.size = spriteRenderer.bounds.size;
            
            // enemy script
            EnemyBasic normalEnemyScript = enemyGameObject.AddComponent<EnemyBasic>();
            normalEnemyScript.health = normalEnemyData.Health;
            normalEnemyScript.lifeItems = normalEnemyData.LifeItemDrop;
            normalEnemyScript.powerItems = normalEnemyData.PowerItemDrop;
            normalEnemyScript.scoreItems = normalEnemyData.ScoreItemDrop;
            normalEnemyScript.scoreWorth = normalEnemyData.Score;
            normalEnemyScript.bigPowerItems = normalEnemyData.BigPowerItemDrop;
            
            return enemyGameObject;
        }
    }
}
