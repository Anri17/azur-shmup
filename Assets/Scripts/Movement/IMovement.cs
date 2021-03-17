using System.Collections;

namespace AzurProject.Movement
{
    public interface IMovement
    {
        IEnumerator MoveCoroutine();
    }
}
