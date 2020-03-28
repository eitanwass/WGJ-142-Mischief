using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundFXSlider;
    public Slider voiceSlider;

    [RuntimeInitializeOnLoadMethod]
    private static void Initialze()
    {
        PlayerPrefs.SetFloat("music_volume", 1);
        PlayerPrefs.SetFloat("fx_volume", 1);
        PlayerPrefs.SetFloat("voice_volume", 1);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music_volume");
        soundFXSlider.value =  PlayerPrefs.GetFloat("fx_volume");
        voiceSlider.value = PlayerPrefs.GetFloat("voice_volume");
    }

    private void OnGUI()
    {
        PlayerPrefs.SetFloat("music_volume", musicSlider.value);
        PlayerPrefs.SetFloat("fx_volume", soundFXSlider.value);
        PlayerPrefs.SetFloat("voice_volume", voiceSlider.value);
    }
}
