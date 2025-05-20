using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    public Toggle vSyncToggle;

    private Resolution[] resolutions;

    void Start()
    {
        // Получаем все доступные разрешения
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int currentResolutionIndex = 0;
        var options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (!options.Contains(option))
                options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullscreenToggle.isOn = Screen.fullScreen;
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;
    }

    public void ApplySettings()
    {
        string[] dims = resolutionDropdown.options[resolutionDropdown.value].text.Split('x');
        int width = int.Parse(dims[0]);
        int height = int.Parse(dims[1]);

        bool isFullscreen = fullscreenToggle.isOn;
        bool useVSync = vSyncToggle.isOn;

        Screen.SetResolution(width, height, isFullscreen);
        QualitySettings.vSyncCount = useVSync ? 1 : 0;
    }
}

