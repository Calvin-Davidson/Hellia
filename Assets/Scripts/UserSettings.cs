using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    public const float CameraRotationSpeed = 250.0f;
    
    public static float lookXSensitivity = 2.0f;
    public static float lookYSensitivity = 1.5f;
    public static float cameraZoomSensitivity = 3f;

    public void SetLookXSensitivity(float newValue)
    {
        lookXSensitivity = Mathf.Clamp(newValue, 1, 10f);
    }
    
    public void SetLookYSensitivity(float newValue)
    {
        lookYSensitivity = Mathf.Clamp(newValue, 1, 10f);
    }
    
    public void SetCameraZoomSensitivity(float newValue)
    {
        cameraZoomSensitivity = Mathf.Clamp(newValue, 1, 10f);
    }
}
