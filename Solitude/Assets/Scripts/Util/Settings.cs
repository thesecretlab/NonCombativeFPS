using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides functionality to the pause menu by adjusting playerprefs
/// </summary>

public class Settings : MonoBehaviour
{

    public Slider BGSoundSlider;
    public Slider SFXSoundSlider;
    public Text SaveButtontext;

    void Start()
    {
        CreateKeys();
        BGSoundSlider.value = PlayerPrefs.GetFloat("BGSound");
        SFXSoundSlider.value = PlayerPrefs.GetFloat("SFXSound");
    }

    //sets the values to the slider values
    public void onSave()
    {
        SaveButtontext.text = "Saved!";
        PlayerPrefs.SetFloat("BGSound", BGSoundSlider.value);
        PlayerPrefs.SetFloat("SFXSound", SFXSoundSlider.value);
    }

    void CreateKeys()
    {
        if (!PlayerPrefs.HasKey("BGSound") && !PlayerPrefs.HasKey("SFXSound"))
        {
            PlayerPrefs.SetFloat("BGSound", 1);
            PlayerPrefs.SetFloat("SFXSound", 1);
        }
    }
}
