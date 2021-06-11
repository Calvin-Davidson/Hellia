using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class LevelSelectionButton : MonoBehaviour, IPointerClickHandler
{
    public AudioClip ButtonDenySound;
    public AudioClip ButtonAcceptSound;

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
        if (LevelSystem.HasFinishedLevel("Level_" + (levelId - 1)) || levelId == 1)
        {
            Debug.Log("noice");
            audioSource.clip = ButtonAcceptSound;
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

            audioSource.clip = ButtonAcceptSound;
            OnSetInactive?.Invoke();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (audioSource.clip != null) audioSource.Play();

        if (isActive)
        {
            GameControl.Instance.LoadLevel(levelId);
        }
    }
}
