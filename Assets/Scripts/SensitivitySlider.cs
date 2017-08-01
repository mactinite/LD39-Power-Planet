using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SensitivitySlider : MonoBehaviour {

    public Slider slider;
    private SimpleSmoothMouseLook mouseLook;
	// Use this for initialization
	void Start () {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mouseLook = Camera.main.GetComponent<SimpleSmoothMouseLook>();
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        mouseLook.sensitivity.x = slider.value;
        mouseLook.sensitivity.y = slider.value;
    }
}
