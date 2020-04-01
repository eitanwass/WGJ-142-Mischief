using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Options : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundFXSlider;
    public Slider voiceSlider;
    public Toggle fullscreen;
    public TMP_Dropdown resolutions;

    private static List<(int, int)> res_options = new List<(int, int)>();

    [RuntimeInitializeOnLoadMethod]
    private static void Initialze()
    {
        PlayerPrefs.SetFloat("music_volume", 1);
        PlayerPrefs.SetFloat("fx_volume", 1);
        PlayerPrefs.SetFloat("voice_volume", 1);
        foreach (var item in Screen.resolutions)
        {
            if (!res_options.Contains((item.width, item.height)))
                res_options.Add((item.width, item.height));
        }
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music_volume");
        soundFXSlider.value =  PlayerPrefs.GetFloat("fx_volume");
        voiceSlider.value = PlayerPrefs.GetFloat("voice_volume");
        fullscreen.isOn = Screen.fullScreen;

        resolutions.ClearOptions();
        resolutions.AddOptions(res_options.ConvertAll((x) => x.Item1 + "x" + x.Item2));
        resolutions.value = res_options.IndexOf((Screen.width, Screen.height));
    }

    private void OnGUI()
    {
        PlayerPrefs.SetFloat("music_volume", musicSlider.value);
        PlayerPrefs.SetFloat("fx_volume", soundFXSlider.value);
        PlayerPrefs.SetFloat("voice_volume", voiceSlider.value);

        (int, int) res = res_options[resolutions.value];
        Screen.SetResolution(res.Item1, res.Item2, fullscreen.isOn);
    }
}
