using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensController : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    // Start is called before the first frame update
    void Start()
    {
        if (slider)
            slider.value = 5f;//PlayerPrefs.GetFloat("sens", 100f);
        //MouseComponent.mouseSens = slider.value;
    }

    public void ChangeSlider(float v)
    {
        sliderValue = v;
        PlayerPrefs.SetFloat("sens", sliderValue);
        //MouseComponent.mouseSens = slider.value;
        PlayerPrefs.Save();
    }
}
