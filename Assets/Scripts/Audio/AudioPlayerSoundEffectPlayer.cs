using System;
using System.Collections;
using System.Collections.Generic;
using AzurProject;
using UnityEngine;

namespace AzurProject
{
    public enum AudioPlayerSoundEffect
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
    
    public class AudioPlayerSoundEffectPlayer : MonoBehaviour
    {
        public AudioPlayerSoundEffect soundEffect;

        private AudioPlayer _audioPlayer;
        
        private void Awake()
        {
            _audioPlayer = AudioPlayer.Instance;
        }

        public void PlaySfx()
        {
            switch (soundEffect)
            {
                case AudioPlayerSoundEffect.PAUSE: _audioPlayer.PlaySfx(_audioPlayer.pauseSfx); break;
                case AudioPlayerSoundEffect.RESUME: _audioPlayer.PlaySfx(_audioPlayer.resumeSfx); break;
                case AudioPlayerSoundEffect.ENEMY_HIT: _audioPlayer.PlaySfx(_audioPlayer.enemyHitSfx); break;
                case AudioPlayerSoundEffect.ENEMY_DEATH: _audioPlayer.PlaySfx(_audioPlayer.enemyDeathSfx); break;
                case AudioPlayerSoundEffect.ENEMY_SHOOT: _audioPlayer.PlaySfx(_audioPlayer.enemyShootSfx); break;
                case AudioPlayerSoundEffect.BUTTON_CLICK: _audioPlayer.PlaySfx(_audioPlayer.selectSfx); break;
                case AudioPlayerSoundEffect.PLAYER_DEATH: _audioPlayer.PlaySfx(_audioPlayer.playerDeathSfx); break;
                case AudioPlayerSoundEffect.PLAYER_SHOOT: _audioPlayer.PlaySfx(_audioPlayer.playerShootSfx); break;
            }
        }
    }
}
