using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelSelectionButton : MonoBehaviour, IPointerClickHandler
{
    public SoundLibrary ButtonAcceptCue;
    public AudioClip ButtonAcceptSound;
    public SoundLibrary ButtonDenyCue;
    public AudioClip ButtonDenySound;

    public GameObject activeEffect;
    public GameObject inactiveEffect;

    public int levelId;

    public UnityEvent OnSetInactive;
    public UnityEvent OnSetActive;

    private AudioSource audioSource;
    private bool isActive = false;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (LevelSystem.HasFinishedLevel("Level_" + (levelId - 1)) || levelId == 1 || LevelSystem.HasFinishedLevel("Level_" + levelId))
        {
            audioSource.clip = ButtonAcceptSound;
            System.Random rnd = new System.Random();
            int index = rnd.Next(1, ButtonAcceptCue.clips.Length);
            audioSource.clip = ButtonAcceptCue.clips[index];
            if (activeEffect != null) activeEffect.SetActive(true);
            if (inactiveEffect != null) inactiveEffect.SetActive(false);
            isActive = true;
            OnSetActive?.Invoke();
        }
        else
        {
            isActive = false;
            if (activeEffect != null) activeEffect.SetActive(false);
            if (inactiveEffect != null) inactiveEffect.SetActive(true);

            audioSource.clip = ButtonDenySound;
            System.Random rnd = new System.Random();
            int index = rnd.Next(1, ButtonDenyCue.clips.Length);
            audioSource.clip = ButtonDenyCue.clips[index];
            OnSetInactive?.Invoke();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource.clip != null) audioSource.Play();

        if (isActive)
        { 
            SceneManager.LoadScene("Level_" + levelId);
        }
    }
}
