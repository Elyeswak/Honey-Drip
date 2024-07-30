using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _OneShotSFXSource;
    [SerializeField] private AudioSource _loopSFXSource;
    [SerializeField] private List<AudioClip> _audioSources;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            //remove this component
            Destroy(gameObject);
    }

    public static void PlayMusic(string name)
    {
        //stop current music
        instance._musicSource.Stop();
        AudioClip clip = instance._audioSources.Find(s => s.name == name);
        if (clip == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }
        instance._musicSource.clip = clip;
        instance._musicSource.Play();
    }
    public static void StopMusic()
    {
        instance._musicSource.Stop();
    }

    public static void PlaySFX(string name)
    {
        AudioClip clip = instance._audioSources.Find(s => s.name == name);
        if (clip == null)
        {
            Debug.LogWarning("SFX: " + name + " not found!");
            return;
        }
        instance._OneShotSFXSource.PlayOneShot(clip);
    }

    public static void PlayRandomFrom(string[] SFXNames)
    {
        int index = Random.Range(0, SFXNames.Length);
        PlaySFX(SFXNames[index]);
    }

    public static void PlayLoopSFX(string name)
    {
        AudioClip clip = instance._audioSources.Find(s => s.name == name);
        if (clip == null)
        {
            Debug.LogWarning("SFX: " + name + " not found!");
            return;
        }
        instance._loopSFXSource.clip = clip;
        instance._loopSFXSource.Play();
    }
    public static void StopLoopSFX()
    {
        instance._loopSFXSource.Stop();
    }
    
}
