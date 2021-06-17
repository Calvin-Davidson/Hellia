using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CutsceneLoader : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string mainMenuSceneName;

    public UnityEvent onSceneLoaded = new UnityEvent();

    private bool isDone = false;
    private bool isSkipped = false;
    private bool isLoaded = false;
    private void Start()
    {
        videoPlayer.Play();
        InvokeRepeating(nameof(checkOver), .1f, .1f);
        StartCoroutine(LoadSceneAsync());
    }

    private void checkOver()
    {
        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = (long) videoPlayer.frameCount;

        if (playerCurrentFrame < playerFrameCount - 1)
            return;

        isDone = true;
        CancelInvoke(nameof(checkOver));
    }


    public void Skip()
    {
        isSkipped = true;
    }

    IEnumerator LoadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (asyncLoad.progress <= 0.9f && !isDone && !isSkipped)
        {
            if (asyncLoad.progress >= 0.9f && !isLoaded)
            {
                Debug.Log("scene loaded");
                onSceneLoaded?.Invoke();
                isLoaded = true;
            } 
            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
    }
}
