using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : SceneService
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private AudioObject[] musics = new AudioObject[] { };
    [SerializeField] private AudioObject[] sfxs = new AudioObject[] { };

    private Dictionary<string, AudioObject> musicDictionary;
    private Dictionary<string, AudioObject> sfxDictionary;

    [Range(0f, 1f)]
    [SerializeField] private float musicVolume = 1f;
    private float maxMusicVolume;

    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;
    private float maxSFXVolume;

    protected override void OnInitialize()
    {
        musicDictionary = musics.ToDictionary(music => music.AudioCode);
        sfxDictionary = sfxs.ToDictionary(sfx => sfx.AudioCode);
    }

    public void PlayMusic(string audioCode)
    {
        var hasAudio = musicDictionary.TryGetValue(audioCode, out var audioObject);
        if (hasAudio)
        {
            maxMusicVolume = audioObject.MaxVolume;
            musicSource.clip = audioObject.AudioClip;
            musicSource.volume = musicVolume * maxMusicVolume;
            musicSource.Play();
        }
    }

    public void PlaySFX(string audioCode)
    {
        var hasAudio = sfxDictionary.TryGetValue(audioCode, out var audioObject);
        if (hasAudio)
        {
            maxSFXVolume = audioObject.MaxVolume;
            var volume = sfxVolume * maxSFXVolume;
            sfxSource.PlayOneShot(audioObject.AudioClip, volume);
        }
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.UnPause();
        }
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
            StartCoroutine(FadeOutMusic());
    }

    private IEnumerator FadeOutMusic()
    {
        while (musicSource.volume > 0)
        {
            musicSource.volume -= Time.deltaTime * 0.25f; // Adjust the fade out speed
            yield return null;
        }

        musicSource.Stop();
    }

    public void PauseSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Pause();
        }
    }

    public void ResumeSFX()
    {
        if (!sfxSource.isPlaying)
        {
            sfxSource.UnPause();
        }
    }

    public void StopSFX()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume * maxMusicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        sfxSource.volume = sfxVolume * maxSFXVolume;
    }
}
