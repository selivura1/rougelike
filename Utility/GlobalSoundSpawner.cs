using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundSpawner : MonoBehaviour
{
    [SerializeField] AudioClip spawned;
    [SerializeField] AudioSource _effectSource, _musicSource;
    private Options options;
    
    void GetReferences()
    {
        options = ReferenceContainer.Options;
    }
    private void Start()
    {
        PlayMusic(spawned, 1, 1);
    }
    public void PlaySoundEffect(AudioClip sound, float pitchMin = 0.9f, float pitchMax = 1.1f)
    {
        if (options == null)
            GetReferences();
        _effectSource.pitch = Random.Range(pitchMin, pitchMax);
        _effectSource.PlayOneShot(sound, options.CurrentConfig.SFX);
    }
    public  void PlayMusic(AudioClip sound, float pitchMin = 0.9f, float pitchMax = 1.1f)
    {
        if (options == null)
            GetReferences();
        _musicSource.pitch = Random.Range(pitchMin, pitchMax);
        _musicSource.PlayOneShot(sound, options.CurrentConfig.Music);
    }
}
