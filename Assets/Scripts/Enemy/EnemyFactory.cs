using System;
using System.ComponentModel;
using System.IO;
using AzurProject.Data;
using AzurProject.Movement;
using AzurProject.Shooting;
using AzurProject.StageEditor;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

namespace AzurProject.Enemy
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
            Sprite sprite = AzurProjectBase.LoadSprite(normalEnemyData.Sprite, 21.333333f);
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
            NormalEnemy normalEnemyScript = enemyGameObject.AddComponent<NormalEnemy>();
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
