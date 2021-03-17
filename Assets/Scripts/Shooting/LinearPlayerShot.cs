namespace AzurProject.Shooting
{
    public class LinearPlayerShot : LinearShot
    {
        private void Start()
        {
            FireCoroutine = StartCoroutine(ShootCoroutine());
        }
    }
}
