using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown dropdownResolution;
    Resolution[] resolutions;

    private void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = false;
        }
        CheckResolution();
    }
    public void TurnOff_FullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void CheckResolution()
    {
        resolutions = Screen.resolutions;
        dropdownResolution.ClearOptions();
        List<string> options = new List<string>();
        int CurrentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolution = i;
            }
        }
        dropdownResolution.AddOptions(options);
        dropdownResolution.value = CurrentResolution;
        dropdownResolution.RefreshShownValue();

        dropdownResolution.value = PlayerPrefs.GetInt("NumResolution", 0);
    }

    public void ChangeResolution(int IndexResolution)
    {
        PlayerPrefs.SetInt("NumResolution", dropdownResolution.value);

        Resolution resolution = resolutions[IndexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
