using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundFade : MonoBehaviour
{
    [SerializeField] private float fadeTime;


    public void StartFade()
    {
        StartCoroutine(StartFade(fadeTime));
    }

    public IEnumerator StartFade(float duration)
    {
        AudioSource source = GetComponent<AudioSource>();
        float currentTime = 0;
        float start = source.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            source.volume = Mathf.Lerp(start, 0.0f, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}