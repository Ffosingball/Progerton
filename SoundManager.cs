using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;

public class SoundManager : MonoBehaviour
{
    //Reference to the listener
    public AudioMixer mixer;
    public AudioSource musicSource, uiSoundSource;
    public AudioClip buttonSound, keyPressedSound, victorySound, lostSound, fallRestartSound, belowZeroRestartSound;
    //Reference to sound
    public MusicLengths[] musics;
    //private Coroutine changeValueSoundPlaying=null;

    private bool musicStoped=false;


    //It starts play background music
    private void Start()
    {
        StartCoroutine(backgroundMusic());
    }


    //Infinite loop to play background music
    private IEnumerator<WaitForSeconds> backgroundMusic()
    {
        while (true)
        {
            PlayMusic(musics[UnityEngine.Random.Range(0, musics.Length)].music);

            while(musicSource.isPlaying || musicStoped)
            {
                yield return new WaitForSeconds(0.1f); 
            }
        }
    }


    //This method strart play a music
    private void PlayMusic(AudioClip soundClip)
    {
        musicSource.clip = soundClip;
        musicSource.Play();
        //Debug.Log("Playing music");
    }


    //This method stops all music
    public void PauseMusic()
    {   
        if(musicSource.isPlaying)
        {
            musicSource.Pause();
            musicStoped = true;
        }
    }


    public void ResumeMusic()
    {   
        if(musicStoped)
        {
            musicSource.UnPause();
            musicStoped = false;
        }
    }


    public void playButtonSound()
    {
        uiSoundSource.PlayOneShot(buttonSound);
    }


    public void playKeyPressedSound()
    {
        uiSoundSource.PlayOneShot(keyPressedSound);
    }


    public void playVictorySound()
    {
        uiSoundSource.PlayOneShot(victorySound);
    }


    public void playLostSound()
    {
        uiSoundSource.PlayOneShot(lostSound);
    }

    public void playFallDamageSound()
    {
        uiSoundSource.PlayOneShot(fallRestartSound);
    }


    public void playfallBelowZeroSound()
    {
        uiSoundSource.PlayOneShot(belowZeroRestartSound);
    }


    public void updateMusicVolume(float musicVolume, float offset=0)
    {
        mixer.SetFloat("MusicVolume", (Mathf.Log10(musicVolume) * 20)+offset);
    }


    public void updateSoundVolume(float soundVolume)
    {
        mixer.SetFloat("SoundVolume", Mathf.Log10(soundVolume) * 20);
    }
}


[Serializable]
public struct MusicLengths
{
    public AudioClip music;
    public int length;
}