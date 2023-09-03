using UnityEngine;
using UnityEditor;

namespace AzurShmup.Editor
{
    public static class EditorBase
    {
        public static void InstantiatePrefab(GameObject prefab, Vector3 pos)
        {
            GameObject gameObj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (gameObj != null)
            {
                gameObj.transform.position = pos;
            }
        }
    }
}
