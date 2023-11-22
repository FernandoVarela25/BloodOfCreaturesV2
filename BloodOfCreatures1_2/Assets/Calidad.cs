using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calidad : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int quality;

    void Start()
    {
        quality = PlayerPrefs.GetInt("NumQuality", 3);
        dropdown.value = quality;
        AdjustQuality();
    }

    public void AdjustQuality()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("NumQuality", dropdown.value);
        quality = dropdown.value;
    }
}
