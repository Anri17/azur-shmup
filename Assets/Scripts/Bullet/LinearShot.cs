using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class LinearShot : Shot
    {
        public float startDelay;
        public float loopDelay;
        public float bulletCount;

        protected override Coroutine Shoot()
        {
            return StartCoroutine(_shotManager.LinearShotCoroutine(_bulletSettings, startDelay, loopDelay, bulletCount));
        }
    }
}
