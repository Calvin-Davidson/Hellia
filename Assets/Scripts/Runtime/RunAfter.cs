using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunAfter : MonoBehaviour
{
    public float after;
    public bool runOnce = false;
    public UnityEvent unityEvent = new UnityEvent(); 
    private float currentTime;
    private bool _hasRun = false;
    
    private void OnEnable()
    {
        currentTime = 0f;
        _hasRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > after && ((runOnce && !_hasRun) || !runOnce))
        {
            _hasRun = true;
            unityEvent?.Invoke();
        }
    }
}
