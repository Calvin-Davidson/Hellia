using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSoundHandler : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private SoundLibrary[] sounds;
    [SerializeField] private AudioClip defaultClip;
    private AudioSource source;
    private SoundSurface _currentSoundSurface = SoundSurface.Default;
    private void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (hit.transform.gameObject.TryGetComponent(out BlockSoundSurface blockSurface))
            {
                _currentSoundSurface = blockSurface.GetSoundSurface();
            }
        }
    }
    public void PlayWalkSound()
    {
        source.clip = defaultClip;
        foreach (SoundLibrary soundLibrary in sounds)
        {
            if(soundLibrary.surface == _currentSoundSurface)
            {
                System.Random rnd = new System.Random();
                int index = rnd.Next(1, soundLibrary.clips.Length);
                source.clip = soundLibrary.clips[index];
            }
        }
        source.Play();
    }
}
