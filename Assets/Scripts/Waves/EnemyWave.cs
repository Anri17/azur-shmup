using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AzurProject
{
    [CreateAssetMenu(fileName = "New Enemy Wave", menuName = "Wave/Enemy Wave")]
    public class EnemyWave : Wave
    {
        [SerializeField] private GameObject waveGameObject;
        [SerializeField] float waveLifeCycle;

        public GameObject WaveGameObject { get => waveGameObject; }
        public float LifeCycle { get => waveLifeCycle; }
    }
}
