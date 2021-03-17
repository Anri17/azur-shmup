using AzurProject.Core;
using UnityEngine;

namespace AzurProject.Interface
{
    public class WaveGameObject : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            // Display play field for debuging purposes
            Gizmos.DrawLine(GameManager.GAME_FIELD_TOP_LEFT, GameManager.GAME_FIELD_TOP_RIGHT);
            Gizmos.DrawLine(GameManager.GAME_FIELD_TOP_RIGHT, GameManager.GAME_FIELD_BOTTOM_RIGHT);
            Gizmos.DrawLine(GameManager.GAME_FIELD_BOTTOM_RIGHT, GameManager.GAME_FIELD_BOTTOM_LEFT);
            Gizmos.DrawLine(GameManager.GAME_FIELD_BOTTOM_LEFT, GameManager.GAME_FIELD_TOP_LEFT);
        }
    }
}
