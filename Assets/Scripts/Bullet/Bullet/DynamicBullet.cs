using System.Collections;
using UnityEngine;

namespace AzurProject.Bullet
{
    public class DynamicBullet : Bullet
    {
        private void Start()
        {
            ChangeSpeed();
        }
        
        public void ChangeSpeed()
        {
            StartCoroutine(ChangeSpeedCoroutine());
        }

        public IEnumerator ChangeSpeedCoroutine()
        {
            BulletSettings bulletSettings = new BulletSettings();
            Speed = bulletSettings.speed;

            for (int i = 0; i < bulletSettings.bulletSpeedTransitions.Length; i++)
            {
                
                BulletSpeedTransition bulletSpeedTransition = bulletSettings.bulletSpeedTransitions[i];
                yield return new WaitForSeconds(bulletSpeedTransition.transitionDelay);
                
                float yIntercept = speed;
                float slope = (bulletSpeedTransition.targetSpeed - yIntercept) / bulletSpeedTransition.transitionDuration;
                for (float time = 0; time < bulletSpeedTransition.transitionDuration; time += Time.deltaTime)
                {
                    Speed = slope * time + yIntercept;
                    yield return null;
                }

                Speed = bulletSpeedTransition.targetSpeed;
                yield return null;
            }
            
        }
    }
}
