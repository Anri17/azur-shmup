using UnityEngine;

namespace AzurProject.Bullet
{
    public class LinearBullet : Bullet
    {
        private void Update()
        {
            // Set the Angle
            transform.rotation = Quaternion.Euler(0, 0, Angle + 180);

            // Move the Bullet
            transform.Translate(Vector3.up * ((_speed / 10) * Time.deltaTime), Space.Self);
        }
    }
}
