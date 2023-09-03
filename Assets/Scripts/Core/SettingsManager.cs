using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AzurShmup.Core
{
    public class SettingsManager : SingletonPersistent<SettingsManager>
    {
        public SettingsData Data { get; set; }

        private void Awake()
        {
            MakeSingleton();
        }

        private void Start()
        {
            if (!File.Exists($"{Application.persistentDataPath}/settings.json"))
            {
                string json = JsonUtility.ToJson(new SettingsData());
                File.WriteAllText($"{Application.persistentDataPath}/settings.json", json);
            }
        }

        public void SaveSettings()
        {
            string json = JsonUtility.ToJson(Data);
            File.WriteAllText($"{Application.persistentDataPath}/settings.json", json);
        }

        public void SaveSettings(SettingsData data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText($"{Application.persistentDataPath}/settings.json", json);
        }

        public void LoadSettings()
        {
            string settingsPath = $"{Application.persistentDataPath}/settings.json";

            string json = File.ReadAllText(settingsPath);

            Data = JsonUtility.FromJson<SettingsData>(json);
        }
    }
}
