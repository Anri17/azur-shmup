using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurShmup.SpellAttacks
{
    public class SpellAttack : MonoBehaviour
    {
        public GameObject bossShots;    // objects that are parented to the boss
        public GameObject staticShots;  // objects that are parented to the scene

        public void Awake()
        {
            // deactivate spells to they can be setup in the Boss
            bossShots.SetActive(false);
            staticShots.SetActive(false);
        }

        public void StartSpell()
        {
            bossShots.SetActive(true);
            staticShots.SetActive(true);
        }
    }
}
