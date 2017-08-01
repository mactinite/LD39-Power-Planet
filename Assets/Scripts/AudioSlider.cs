using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour {

    public Slider slider;
    public AudioMixer mixer;
    public string paramName = "MusicVol";
	// Use this for initialization
	void Start () {
        //Adds a listener to the main slider and invokes a method when the value changes.
        float value = 0;
        mixer.GetFloat(paramName, out value);
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        mixer.SetFloat(paramName, slider.value);
    }
}
