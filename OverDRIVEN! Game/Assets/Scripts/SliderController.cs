using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderController : MonoBehaviour {

    public Slider mainSlider;

    public Camera mainCamera;
    public CameraController camController;

    public void Start() {
        mainSlider.wholeNumbers = true;
        mainSlider.minValue = 50;
        mainSlider.maxValue = 120;
        mainSlider.value = 60;

        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(delegate {
            ValueChanged();
        });
    }

    // Invoked when the value of the slider changes.
    public void ValueChanged() {
        mainCamera.fieldOfView = mainSlider.value;
        camController.height = 0.2f + ((mainSlider.value / 200) - 0.3f);
    }
}
