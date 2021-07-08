using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePause : MonoBehaviour
{
    private Canvas pauseCanvas;
    private bool _isPaused = false;

    public UnityEvent onPause = new UnityEvent();
    public UnityEvent onUnpause = new UnityEvent();
    
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
        onPause?.Invoke();
        Time.timeScale = 0.0f;
        pauseCanvas.enabled = true;
    }

    public void StopPause()
    {
        _isPaused = false;
        onUnpause?.Invoke();
        Time.timeScale = 1.0f;
        pauseCanvas.enabled = false;
    }
}
