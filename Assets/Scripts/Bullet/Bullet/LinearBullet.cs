using UnityEngine;

namespace AzurProject.Bullet
{
    public class LinearBullet : Bullet
    {
        private void Start()
        {
            // Speed = bulletSettings.speed;
        }
        
        private void Update()
        {
            // Set the Angle
            transform.rotation = Quaternion.Euler(0, 0, Angle + 180);

            // Move the Bullet
            transform.Translate(Vector3.up * ((speed / 10) * Time.deltaTime), Space.Self);
        }
    }
}
