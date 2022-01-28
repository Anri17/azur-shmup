using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class LinearShot : Shot
    {
        protected override Coroutine Shoot()
        {
            return StartCoroutine(_shotManager.LinearPatternCoroutine(_bulletSettings, startDelay, loopDelay, bulletCount));
        }
    }
}
