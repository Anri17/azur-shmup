using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace AzurProject
{
    public class AudioPlayer : MonoBehaviour
    {
        public static AudioPlayer Instance { get; private set; }

        public const string MASTER_VOLUME_LEVEL = "MasterVolumeLevel";
        public const string MUSIC_VOLUME_LEVEL = "MusicVolumeLevel";
        public const string EFFECTS_VOLUME_LEVEL = "EffectsVolumeLevel";

        [Header("Audio Sources")]
        [SerializeField] private AudioMixer mixer;
        public AudioSource bgmAudioSource;
        public AudioSource sfxAudioSource;
        
        [Header("Sound Effects")]
        public AudioClip pauseSfx;
        public AudioClip resumeSfx;
        public AudioClip enemyHitSfx;
        public AudioClip enemyDeathSfx;
        public AudioClip playerDeathSfx;
        public AudioClip playerShootSfx;

        private double _bmgLoopTime;
        private SettingsData _settings;
        private SettingsManager _settingsManager;

        public bool Loop { get; set; } = true;

        public float MasterVolumeLevel
        {
            get => _settingsManager.Settings.masterVolumeLevel;
            set
            {
                SettingsData settingsData = _settings;
                settingsData.masterVolumeLevel = value;
                _settingsManager.SaveSettings(settingsData);
                mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
            }
        }

        public float BgmVolumeLevel
        {
            get => _settingsManager.Settings.musicVolumeLevel;
            set
            {
                SettingsData settingsData = _settings;
                settingsData.musicVolumeLevel = value;
                _settingsManager.SaveSettings(settingsData);
                mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            }
        }

        public float SfxVolumeLevel
        {
            get => _settingsManager.Settings.effectsVolumeLevel;
            set
            {
                SettingsData settingsData = _settings;
                settingsData.effectsVolumeLevel = value;
                _settingsManager.SaveSettings(settingsData);
                mixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
            }
        }

        private void Awake()
        {
            MakeSingleton();
            _settingsManager = SettingsManager.Instance;
        }

        private void Start()
        {
            _settings = _settingsManager.Settings;
            
            // Init Audio Settings by setting themselves up
            MasterVolumeLevel = MasterVolumeLevel;
            BgmVolumeLevel = BgmVolumeLevel;
            SfxVolumeLevel = SfxVolumeLevel;
        }

        private void Update()
        {
            if (bgmAudioSource.clip != null)
            {
                if (bgmAudioSource.time >= bgmAudioSource.clip.length)
                {
                    bgmAudioSource.Stop();
                    if (Loop)
                    {
                        bgmAudioSource.time = (float) _bmgLoopTime;
                    }
                    else
                    {
                        bgmAudioSource.time = 0;
                    }

                    bgmAudioSource.Play();
                }
            }
        }
        
        private void MakeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlaySfx(AudioClip audioClip)
        {
            sfxAudioSource.PlayOneShot(audioClip);
        }

        public void PlayMusic(AudioClip musicFile, double loopTime)
        {
            bgmAudioSource.Stop();
            bgmAudioSource.loop = true;
            bgmAudioSource.time = 0;
            bgmAudioSource.clip = musicFile;
            bgmAudioSource.loop = false;
            _bmgLoopTime = (double) loopTime / 1000.0;
            bgmAudioSource.Play();
        }

        public void StopMusic()
        {
            bgmAudioSource.Stop();
        }

        public void PauseMusic()
        {
            bgmAudioSource.Pause();
        }

        public void ResumeMusic()
        {
            bgmAudioSource.Play();
        }

        public void FadeMusicOut()
        {
            StartCoroutine(FadeMusicOutCoroutine());
        }

        private IEnumerator FadeMusicOutCoroutine()
        {
            for (float t = 1; t > 0; t -= Time.deltaTime)
            {
                bgmAudioSource.volume = t;
                yield return null;
            }
            StopMusic();
            bgmAudioSource.volume = 1;
            yield return null;
        }
    }
}