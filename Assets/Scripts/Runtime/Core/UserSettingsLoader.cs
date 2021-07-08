using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingsLoader
{
    /**
     * get's the default settings and saves them in the playerPrefs if they did not exist already.
     */
    [RuntimeInitializeOnLoadMethod]
    static void LoadSettings()
    {
        if (!PlayerPrefs.HasKey(SettingType.MouseSensitivityX.ToString()))
            PlayerPrefs.SetFloat(SettingType.MouseSensitivityX.ToString(), UserSettings.LookXSensitivity);
        if (!PlayerPrefs.HasKey(SettingType.MouseSensitivityY.ToString()))
            PlayerPrefs.SetFloat(SettingType.MouseSensitivityY.ToString(), UserSettings.LookYSensitivity);
        if (!PlayerPrefs.HasKey(SettingType.MouseZoomSensitivity.ToString()))
          PlayerPrefs.SetFloat(SettingType.MouseZoomSensitivity.ToString(), UserSettings.CameraZoomSensitivity);
    }
}
