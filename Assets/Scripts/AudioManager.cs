using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get => _instance;
    }
    
    public AudioSource _bgm;
    [SerializeField] private AudioSource[] _audioSources;
    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        _audioSources = new AudioSource[clips.Length];

        for (int i = 0; i < clips.Length; i++)
        {
            AudioSource newAudio = new GameObject(clips[i].name).AddComponent<AudioSource>();
            newAudio.transform.SetParent(transform);
            newAudio.clip = clips[i];
            _audioSources[i] = newAudio;
        }
    }

    private void Start()
    {
        _bgm.Stop();
    }

    public void PlayAudio(int i, float volume = 1, float pitch = 1)
    {
        if (i < 0 || i >= clips.Length) return;
        AudioSource _audioSource = _audioSources[i];
        _audioSource.volume = volume;
        _audioSource.pitch = pitch;
        _audioSource.Play();
    }
}
