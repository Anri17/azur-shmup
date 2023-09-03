using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;

namespace AzurShmup.Core
{
    public enum AudioSFX
    {
        PAUSE,
        BUTTON_CLICK,
        RESUME,
        ENEMY_HIT,
        ENEMY_DEATH,
        ENEMY_SHOOT,
        PLAYER_DEATH,
        PLAYER_SHOOT
    }

    public class AudioPlayer : SingletonPersistent<AudioPlayer>
    {
        public const string MASTER_VOLUME_LEVEL = "MasterVolumeLevel";
        public const string MUSIC_VOLUME_LEVEL = "MusicVolumeLevel";
        public const string EFFECTS_VOLUME_LEVEL = "EffectsVolumeLevel";

        public AudioSource MusicPlayer { get => bgmAudioSource; }

        [Header("Audio Sources")]
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource sfxAudioSource;
        
        [Header("Sound Effects")]
        public AudioClip pauseSfx;
        public AudioClip resumeSfx;
        public AudioClip buttonClickSfx;
        public AudioClip enemyHitSfx;
        public AudioClip enemyShootSfx;
        public AudioClip enemyDeathSfx;
        public AudioClip playerDeathSfx;
        public AudioClip playerShootSfx;

        private double _bmgLoopTime;

        private float masterVolumeLevel;
        private float bgmVolumeLevel;
        private float sfxVolumeLevel;

        public bool Loop { get; set; } = true;

        public float MasterVolumeLevel
        {
            get => masterVolumeLevel;
            set
            {
                masterVolumeLevel = value;
                mixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
            }
        }

        public float BgmVolumeLevel
        {
            get => bgmVolumeLevel;
            set
            {
                bgmVolumeLevel = value;
                mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            }
        }

        public float SfxVolumeLevel
        {
            get => sfxVolumeLevel;
            set
            {
                sfxVolumeLevel = value;
                mixer.SetFloat("EffectsVolume", Mathf.Log10(value) * 20);
            }
        }

        private void Awake()
        {
            MakeSingleton();
        }

        public void SetVolumeLevels(float master, float bgm, float sfx)
        {
            MasterVolumeLevel = master;
            BgmVolumeLevel = bgm;
            SfxVolumeLevel = sfx;
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

        public void PlaySfx(AudioClip audioClip)
        {
            sfxAudioSource.PlayOneShot(audioClip);
        }

        public void PlaySfx(AudioSFX audioSfx)
        {
            switch (audioSfx)
            {
                case AudioSFX.PAUSE:        sfxAudioSource.PlayOneShot(pauseSfx); break;
                case AudioSFX.RESUME:       sfxAudioSource.PlayOneShot(resumeSfx); break;
                case AudioSFX.ENEMY_HIT:    sfxAudioSource.PlayOneShot(enemyHitSfx); break;
                case AudioSFX.ENEMY_DEATH:  sfxAudioSource.PlayOneShot(enemyDeathSfx); break;
                case AudioSFX.ENEMY_SHOOT:  sfxAudioSource.PlayOneShot(enemyShootSfx); break;
                case AudioSFX.BUTTON_CLICK: sfxAudioSource.PlayOneShot(buttonClickSfx); break;
                case AudioSFX.PLAYER_DEATH: sfxAudioSource.PlayOneShot(playerDeathSfx); break;
                case AudioSFX.PLAYER_SHOOT: sfxAudioSource.PlayOneShot(playerShootSfx); break;
            }
        }

        public void PlayMusic(AudioClip musicFile, double loopTime)
        {
            bgmAudioSource.volume = 1; // In Case FadeOutMusic() bugs out and it's not at 1.
            bgmAudioSource.Stop();
            bgmAudioSource.loop = true;
            bgmAudioSource.time = 0;
            bgmAudioSource.clip = musicFile;
            bgmAudioSource.loop = false;
            _bmgLoopTime = (double)loopTime / 1000.0;
            bgmAudioSource.Play();
        }

        public void PlayMusic(MusicClip musicClip)
        {
            bgmAudioSource.Stop();
            bgmAudioSource.loop = true;
            bgmAudioSource.time = 0;
            bgmAudioSource.clip = musicClip.musicClip;
            bgmAudioSource.loop = false;
            _bmgLoopTime = (double)musicClip.loopStart / 1000.0;
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

        public void FadeOutMusic()
        {
            StartCoroutine(FadeMusicOutCoroutine());
        }

        private IEnumerator FadeMusicOutCoroutine()
        {
            for (float t = 1; t > 0; t -= Time.deltaTime*2)
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