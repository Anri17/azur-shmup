using System.Collections;

namespace AzurShmup.Movement
{
    public interface IMovement
    {
        IEnumerator MoveCoroutine();
    }
}
