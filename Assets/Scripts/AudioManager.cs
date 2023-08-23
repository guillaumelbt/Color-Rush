using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private VolumeScriptable volumeScriptable;

    public float GlobalVolume => volumeScriptable.globalVolume;
    public float MusicVolume => volumeScriptable.musicVolume;
    public float SfxVolume => volumeScriptable.sfxVolume;
    
    public AudioMixerGroup musicMixerGroup;
    public AudioMixerGroup sfxMixerGroup;

    public Sound[] sounds;
    
    public static AudioManager instance;

    private void Awake()
    {
        //vérifie si il y a bien une seule instance, sinon destroy
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            Debug.Log(s.source.volume);
            Debug.Log(s.volume);
            s.source.volume = s.volume;
            Debug.Log(s.source.volume);
            Debug.Log(s.volume);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            switch (s.audioType)
            {
                case Sound.AudioTypes.sfx:
                    s.source.outputAudioMixerGroup = sfxMixerGroup;
                    break;
                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }
        }
    }

    private void Start()
    {
        UpdateGlobalVolume(GlobalVolume);
        UpdateMusicVolume(MusicVolume);
        UpdateSFXVolume(SfxVolume);
    }

    public void UpdateGlobalVolume(float volume)
    {
        volumeScriptable.globalVolume = volume;
        foreach (var s in sounds.Where(s => s.audioType == Sound.AudioTypes.sfx))
        {
            s.source.volume = s.source.volume * volumeScriptable.sfxVolume * volumeScriptable.globalVolume;
        }
        foreach (var s in sounds.Where(s => s.audioType == Sound.AudioTypes.music))
        {
            s.source.volume = s.source.volume * volumeScriptable.musicVolume * volumeScriptable.globalVolume;
        }
    }
    
    public void UpdateMusicVolume(float volume)
    {
        volumeScriptable.musicVolume = volume;
        foreach (var s in sounds.Where(s => s.audioType == Sound.AudioTypes.music))
        {
            s.source.volume = s.source.volume * volumeScriptable.musicVolume * volumeScriptable.globalVolume;
        }
    }
    
    public void UpdateSFXVolume(float volume)
    {
        volumeScriptable.sfxVolume = volume;
        foreach (var s in sounds.Where(s => s.audioType == Sound.AudioTypes.sfx))
        {
            s.source.volume = s.source.volume * volumeScriptable.sfxVolume * volumeScriptable.globalVolume;
        }
    }

    public void Play(string name)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }
    public void SfxMixerVolumeOn()
    {
        sfxMixerGroup.audioMixer.SetFloat("Exposed Sfx Volume", 0f);
    }
    public void SfxMixerVolumeOff()
    {
        sfxMixerGroup.audioMixer.SetFloat("Exposed Sfx Volume", -80f);
    }
    public void MusicMixerVolumeOn()
    {
        musicMixerGroup.audioMixer.SetFloat("Exposed Music Volume", 0f);
    }
    public void MusicMixerVolumeOff()
    {
        musicMixerGroup.audioMixer.SetFloat("Exposed Music Volume", -80f);
    }
}
