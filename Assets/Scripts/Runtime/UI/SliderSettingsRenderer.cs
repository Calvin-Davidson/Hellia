using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderSettingsRenderer : MonoBehaviour
{
    private Slider slider;

    public SettingType settingType;
    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Render();
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        slider.value = PlayerPrefs.GetFloat(settingType.ToString());
    }
}
