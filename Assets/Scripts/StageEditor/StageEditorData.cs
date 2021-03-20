using System.Collections.Generic;
using AzurProject.Data;
using UnityEngine;

namespace AzurProject.StageEditor
{
    public class StageEditorData
    {
        enum EditorObjectType
        {
            NormalEnemy,
            BossEnemy,
            Player
        };

        public List<BaseData> Data = new List<BaseData>();
    }
}
