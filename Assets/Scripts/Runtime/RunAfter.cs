using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunAfter : MonoBehaviour
{
    public float after;
    public UnityEvent unityEvent = new UnityEvent(); 
    private float currentTime;

    private void OnEnable()
    {
        currentTime = 0f;
    }

  
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        
        if (currentTime > after)
        {
            unityEvent?.Invoke();
        }
    }
}
