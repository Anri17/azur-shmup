using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup
{
    [CreateAssetMenu(fileName = "New Spell Attack", menuName = "Boss/Spell Attack")]
    public class SpellAttack : ScriptableObject
    {
        [Header("Details")]
        public int scoreWorth = 100000;
        public float health;
        public float deathTimer;
        [Header("Spell")]
        public float chargeTime = 2;
        public GameObject spellAttack;
        public Vector2 startPosition = new Vector2(0, 0);
        [Header("Drop Count")]
        public int powerItems = 2;
        public int bigPowerItems = 1;
        public int scoreItems = 8;
        public int lifeItems = 0;
    }
}
