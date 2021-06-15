using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    private Canvas pauseCanvas;
    private bool _isPaused = false;

    private void Awake()
    {
        pauseCanvas = GetComponent<Canvas>();
    }

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
        pauseCanvas.enabled = true;
    }

    public void StopPause()
    {
        _isPaused = false;
        Time.timeScale = 1.0f;
        pauseCanvas.enabled = false;
    }
}
