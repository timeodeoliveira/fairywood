using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Resolutions : MonoBehaviour
{
    Resolution[] resolutions;
    public TMP_Dropdown ResolutionsDropDown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        ResolutionsDropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
           
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

           
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }


        ResolutionsDropDown.AddOptions(options);

        ResolutionsDropDown.value = currentResolutionIndex;
        ResolutionsDropDown.RefreshShownValue();
    }

    public void setResolution(int resolutionIndex)
    {
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
        else
        {
            Debug.LogError("Indice de résolution invalide: " + resolutionIndex);
        }
    }
}
