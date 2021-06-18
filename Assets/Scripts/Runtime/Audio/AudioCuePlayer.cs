using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCuePlayer : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private AudioClip defaultClip;
    [SerializeField] private bool playOnStart = true;
    private AudioSource source;
    void Start()
    {
        if(playOnStart) { Play(); }
    }
    public void Play()
    {
        source = this.GetComponent<AudioSource>();
        source.clip = defaultClip;
        System.Random rnd = new System.Random();
        int index = rnd.Next(1, sounds.clips.Length);
        source.clip = sounds.clips[index];
        source.Play();
    }
}
