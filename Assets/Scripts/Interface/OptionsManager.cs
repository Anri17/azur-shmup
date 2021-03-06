using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AzurProject
{
    public class OptionsManager : MonoBehaviour
    {
        private AudioPlayer audioPlayer;

        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider effectsVolumeSlider;

        void Start()
        {
            audioPlayer = AudioPlayer.Instance;
            InitSliderValues();
        }

        void InitSliderValues()
        {
            masterVolumeSlider.value = audioPlayer.MasterVolumeLevel;
            musicVolumeSlider.value = audioPlayer.BgmVolumeLevel;
            effectsVolumeSlider.value = audioPlayer.SfxVolumeLevel;
        }

        public void SetMasterVolumeLevel(float sliderValue)
        {
            audioPlayer.MasterVolumeLevel = sliderValue;
        }

        public void SetMusicVolumeLevel(float sliderValue)
        {
            audioPlayer.BgmVolumeLevel = sliderValue;
        }

        public void SetEffectsVolumeLevel(float sliderValue)
        {
            audioPlayer.SfxVolumeLevel = sliderValue;
        }
    }
}
