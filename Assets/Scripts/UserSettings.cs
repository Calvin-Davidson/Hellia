using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettings : MonoBehaviour
{
    public const float CameraRotationSpeed = 50.0f;
    
    private static float lookXSensitivity = 2.0f;
    private static float lookYSensitivity = 1.5f;
    private static float cameraZoomSensitivity = 3f;

    public void SetLookXSensitivity(float newValue)
    {
        PlayerPrefs.SetFloat(SettingType.MouseSensitivityX.ToString(), newValue);
        lookXSensitivity = newValue;
    }
    
    public void SetLookYSensitivity(float newValue)
    {
        PlayerPrefs.SetFloat(SettingType.MouseSensitivityY.ToString(), newValue);
        lookYSensitivity = newValue;
    }
    
    public void SetCameraZoomSensitivity(float newValue)
    {
        PlayerPrefs.SetFloat(SettingType.MouseZoomSensitivity.ToString(), newValue);
        cameraZoomSensitivity = newValue;
    }

    public static float LookXSensitivity => lookXSensitivity;
    public static float LookYSensitivity => lookYSensitivity;
    public static float CameraZoomSensitivity => cameraZoomSensitivity;
}
