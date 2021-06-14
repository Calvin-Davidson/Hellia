using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class IntroPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string mainMenuSceneName;

    public UnityEvent OnSceneLoaded = new UnityEvent();

    private bool _isDone = false;
    private bool _skip = false;
    private bool _sceneLoaded = false;
    private void Start()
    {
        videoPlayer.Play();
        InvokeRepeating("checkOver", .1f, .1f);
        StartCoroutine(loadSceneAsync());
    }

    private void checkOver()
    {
        long playerCurrentFrame = videoPlayer.frame;
        long playerFrameCount = (long) videoPlayer.frameCount;

        if (playerCurrentFrame < playerFrameCount - 1)
            return;

        _isDone = true;
        CancelInvoke("checkOver");
     
    }


    public void Skip()
    {
        _skip = true;
    }

    IEnumerator loadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while ((!asyncLoad.isDone && !_isDone) || !_skip)
        {
            if (asyncLoad.isDone && !_sceneLoaded)
            {
                OnSceneLoaded?.Invoke();
                _sceneLoaded = true;
            }
             yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
