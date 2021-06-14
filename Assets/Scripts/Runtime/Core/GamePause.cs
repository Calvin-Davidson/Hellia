using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject pauseGameobject;
    private bool _isPaused = false;
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;

        if (_isPaused)
        {
            StopPause();
        } else
        {
            Pause();
        }
    }

    public void Pause()
    {
        _isPaused = true;
        Time.timeScale = 0.0f;
        pauseGameobject.SetActive(true);
    }

    public void StopPause()
    {
        _isPaused = false;
        Time.timeScale = 1.0f;
        pauseGameobject.SetActive(false);
    }
}
