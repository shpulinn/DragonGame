using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private Toggle soundsToggle;

    private void OnEnable()
    {
        soundsToggle.isOn = AudioListener.volume == 1;
    }

    public void ToggleSounds(bool value)
    {
        AudioListener.volume = value ? 1 : 0;
    }
}
