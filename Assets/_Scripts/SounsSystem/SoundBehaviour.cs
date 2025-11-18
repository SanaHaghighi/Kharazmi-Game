using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace SFXSystem.core
{
    public class SoundBehaviour : MonoBehaviour
    {
        public List<PlayingAudio> playingAudios = new List<PlayingAudio>();
        AudioSource BGMSource1;
        AudioSource BGMSource2;
        AudioSource SFXSource;
        List<AudioSource> availableSources = new List<AudioSource>();
        List<PlayingAudio> workingSources = new List<PlayingAudio>();


        AudioSource CurrentBGM => isBGM1 ? BGMSource1 : BGMSource2;
        AudioSource NextBGM => isBGM1 ? BGMSource2 : BGMSource1;
    
    
        bool isBGM1 = true;
        float fadeTime;
        float fadeOutSpeed;
        float fadeInSpeed;
        public void Setup()
        {
            availableSources = new List<AudioSource>();
            workingSources = new List<PlayingAudio>();
            BGMSource1 = gameObject.AddComponent<AudioSource>();
            BGMSource2 = gameObject.AddComponent<AudioSource>();
            for (int i = 0; i < SoundSystemManager.Instance.audioSourceCount; i++)
            {
                availableSources.Add(gameObject.AddComponent<AudioSource>());
            }

            SFXSource = gameObject.AddComponent<AudioSource>();
        }
        public void PlaySFX(AudioClip clip)
        {
            SFXSource.PlayOneShot(clip);
        }

        public void PlaySFX(AudioData data, int delayMs = 0) 
        {
            if (availableSources.Count <= 0)
            {
                Debug.LogError("SoundManager: not enough sources");
                return;
            }

            var s = availableSources[0]; 
            data.SetData(s);
            s.PlayOneShot(s.clip, data.volume / 100);
            availableSources.Remove(s);
            workingSources.Add(new PlayingAudio() { source = s, timer = s.clip.length });
        }

        public void ChangeBGM(AudioData data, float fadeTime = 0)
        {
            if (fadeTime <= 0)
            {
                data.SetData(CurrentBGM,/*Preferences.Instance.MusicMultiplier*/1);
                RestartBGM();
                return;
            }

            this.fadeTime = fadeTime;
            data.SetData(NextBGM);
            NextBGM.Play();
            fadeOutSpeed = CurrentBGM.volume / (fadeTime / 3f);
            fadeInSpeed = NextBGM.volume / ((fadeTime / 3f) * 2);
            NextBGM.volume = 0;
        }
        public void ChangeBGMVolumn(float v)
        {
            CurrentBGM.volume = v;
            NextBGM.volume = v;
        }
        public void PlayBGM()
        {
            CurrentBGM.Play();
        }

        public void StopBGM()
        {
            CurrentBGM.Stop();
        }

        public void PauseBGM()
        {
            CurrentBGM.Pause();
        }

        public void ResumeBGM()
        {
            CurrentBGM.Play();
        }

        public void RestartBGM(bool repeat = true)
        {
            if (CurrentBGM.isPlaying)
                return;
            CurrentBGM.Stop();
            CurrentBGM.Play();
            if(repeat)
                RepeatBGM(CurrentBGM);
        }
        public void RepeatBGM(AudioSource audio)
        {
            if(CurrentBGM==audio)
                audio.Play();
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        public void Tick(float deltaTime)
        {
            foreach (var item in workingSources.ToArray())
            {
                item.timer -= Time.deltaTime;
                if (item.timer <= 0)
                {
                    playingAudios.Remove(item);
                    availableSources.Add(item.source);
                }
            }

            if (fadeTime > 0)
            {
                fadeTime -= Time.deltaTime;

                if (CurrentBGM.clip != null)
                    CurrentBGM.volume -= Time.deltaTime * fadeOutSpeed;
                if (NextBGM.clip != null)
                    NextBGM.volume += Time.deltaTime * fadeInSpeed;

                if (fadeTime <= 0)
                {
                    CurrentBGM.Stop();
                    isBGM1 = !isBGM1;
                }
            }
        }
        [System.Serializable]
        public class PlayingAudio
        {
            public AudioSource source;
            public float timer;
        }

    }
}
