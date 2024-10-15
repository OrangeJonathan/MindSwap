using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
    public static bool IsInitialised { get; private set; }

    public static void Init()
    {
        InitializeSettings();
        IsInitialised = true;
    }

    private static void InitializeSettings()
    {
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {   
            // 100 is default sensitivity
            PlayerPrefs.SetFloat("Sensitivity", 100f);
            PlayerPrefs.Save();
        }
    }

    public static void UpdateSensitivity(float newSensitivity)
    {
        if (newSensitivity >= 1f && newSensitivity <= 1000f)
        {
            PlayerPrefs.SetFloat("Sensitivity", newSensitivity);
            PlayerPrefs.Save();
        }
    }
}
