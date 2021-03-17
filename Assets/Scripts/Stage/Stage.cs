using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Stage", menuName = "Stage/Stage")]
    public class Stage : ScriptableObject
    {
        public string stageName;
        public string stageDescription;
        public StageBanner banner;
        public Wave[] waves;
        public MusicClip stageMusic;
        public MusicClip bossMusic;
        public StageBackground background;
    }
}
