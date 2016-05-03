using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Slider : MonoBehaviour {

    public Slider mainSlider;

    public void Start() {
        //Adds a listener to the main slider and invokes a method when the value changes.
        //mainSlider.onValueChanged.AddListener(delegate {
           // ValueChanged();
       // });
    }

    // Invoked when the value of the slider changes.
    public void ValueChanged() {
       // Debug.Log(mainSlider.value);
    }
}
