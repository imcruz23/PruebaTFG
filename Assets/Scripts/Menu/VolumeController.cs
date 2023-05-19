using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        if (slider)
        {
            slider.value = 0.5f; // PlayerPrefs.GetFloat("audioVolume", 0.5f);
            AudioListener.volume = slider.value;
        }
           
    }

    public void ChangeSlider(float v)
    {
        sliderValue = v;
        PlayerPrefs.SetFloat("audioVolume", sliderValue);
        AudioListener.volume = slider.value;
        PlayerPrefs.Save();
    }
}
