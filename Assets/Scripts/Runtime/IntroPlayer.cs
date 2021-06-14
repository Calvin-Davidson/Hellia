using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string mainMenuSceneName;

    private bool _isDone = false;
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


    IEnumerator loadSceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(mainMenuSceneName);
        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone && !_isDone)
        {
            Debug.Log("not done loading");
            yield return null;
        }

        asyncLoad.allowSceneActivation = true;
    }
}
