using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPushSoundHandler : MonoBehaviour
{
    [SerializeField] private SoundLibrary sounds;
    [SerializeField] private AudioClip defaultClip;
    private AudioSource source;
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        GameControl.Instance.onBlockPushed?.AddListener(OnPushed);
    }

    void OnPushed()
    {
        source.clip = defaultClip;
        System.Random rnd = new System.Random();
        int index = rnd.Next(1, sounds.clips.Length);
        source.clip = sounds.clips[index];
        source.Play();
    }
}
