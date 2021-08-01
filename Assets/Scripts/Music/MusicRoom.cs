using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace AzurProject
{
    public class MusicRoom : MonoBehaviour
    {
        public MusicClip[] musicClips;
        public GameObject musicListContent;

        public Text composerComment;

        public GameObject buttonTemplate;

        public Slider musicProgressBar;

        AudioPlayer audioPlayer;

        private List<GameObject> _buttonClips;

        int contentTotalSize = 0;

        private void Start()
        {
            audioPlayer = AudioPlayer.Instance;
            _buttonClips = CreateMusicList(musicClips);

            SetActiveMusicButton();

            // Initialize MusicProgressBar
            musicProgressBar.maxValue = audioPlayer.bgmAudioSource.clip.length;
        }

        private void Update()
        {
            musicProgressBar.value = audioPlayer.bgmAudioSource.time;
        }

        private List<GameObject> CreateMusicList(MusicClip[] clips)
        {
            List<GameObject> buttonClips = new List<GameObject>();
            int yPos = 0;
            for (int i = 0; i < clips.Length; i++)
            {
                int x = i;
                // Set size of container to fit the new music track
                contentTotalSize = contentTotalSize + 40;

                RectTransform parentObject = musicListContent.GetComponentInParent<RectTransform>();

                musicListContent.GetComponent<RectTransform>().sizeDelta = new Vector2(parentObject.sizeDelta.x, contentTotalSize);


                GameObject tempButton = Instantiate(buttonTemplate, musicListContent.transform);
                tempButton.name = "MusicClip" + (x + 1);
                tempButton.GetComponentInChildren<Text>().text = $"{i + 1}. {clips[x].musicTitle}";
                tempButton.GetComponent<RectTransform>().localPosition += new Vector3(0, yPos, 0);
                tempButton.GetComponent<Button>().onClick.AddListener(delegate { RunMusic(musicClips[x]); });
                buttonClips.Add(tempButton);
                yPos = yPos - 40;
            }

            return buttonClips;
        }

        private void RunMusic(MusicClip clip)
        {
            PlayMusic(clip.musicClip, clip.loopStart);
            composerComment.text = clip.composerComment;

            SetActiveMusicButton();
        }

        private void PlayMusic(AudioClip clip, int loopStart)
        {
            if (audioPlayer.bgmAudioSource.clip != clip)
            {
                audioPlayer.PlayMusic(clip, loopStart);
                musicProgressBar.maxValue = audioPlayer.bgmAudioSource.clip.length;
                StopMusic();
                PlayTrack();
            }
        }

        private void StopMusic()
        {
            if (audioPlayer.bgmAudioSource.clip != null)
            {
                audioPlayer.bgmAudioSource.Stop();
                SetTrackTime(0);
                audioPlayer.bgmAudioSource.time = 0;
            }
        }

        private void PlayTrack()
        {
            if (audioPlayer.bgmAudioSource.clip != null)
            {
                if (!audioPlayer.bgmAudioSource.isPlaying)
                    audioPlayer.bgmAudioSource.Play();
            }
        }

        private void PauseMusic()
        {
            if (audioPlayer.bgmAudioSource.clip != null)
            {
                audioPlayer.PauseMusic();
            }
        }

        private void SetTrackTime(float sliderValue)
        {
            if (sliderValue < musicProgressBar.maxValue && sliderValue > musicProgressBar.minValue)
            {
                audioPlayer.bgmAudioSource.time = sliderValue;
            }
        }

        private void SetActiveMusicButton()
        {
            Button[] buttons = ConvertToButtonArray(_buttonClips);

            for (int i = 0; i < musicClips.Length; i++)
            {
                if (musicClips[i].musicClip == audioPlayer.bgmAudioSource.clip)
                {
                    var thisColors = buttons[i].colors;
                    thisColors.normalColor = new Color(0.5f, 0.5f, 0.2f, 0.5f);
                    thisColors.highlightedColor = new Color(0.5f, 0.5f, 0.2f, 0.5f);
                    thisColors.pressedColor = new Color(0.5f, 0.5f, 0.2f, 0.5f);
                    thisColors.selectedColor = new Color(0.5f, 0.5f, 0.2f, 0.5f);
                    thisColors.disabledColor = new Color(0.5f, 0.5f, 0.2f, 0.5f);
                    buttons[i].colors = thisColors;
                }
                else
                {
                    var thisColors = buttons[i].colors;
                    thisColors.normalColor = new Color(0, 0, 0, 0); ;
                    thisColors.highlightedColor = new Color(0, 0, 0, 0); ;
                    thisColors.pressedColor = new Color(0, 0, 0, 0); ;
                    thisColors.selectedColor = new Color(0, 0, 0, 0); ;
                    thisColors.disabledColor = new Color(0, 0, 0, 0); ;
                    buttons[i].colors = thisColors;
                }
            }
        }

        private Button[] ConvertToButtonArray(List<GameObject> buttonObjects)
        {
            List<Button> resultList = new List<Button>();
            
            foreach (GameObject buttonObject in buttonObjects)
            {
                resultList.Add(buttonObject.GetComponent<Button>());
            }
            return resultList.ToArray();
        }
    }
}
