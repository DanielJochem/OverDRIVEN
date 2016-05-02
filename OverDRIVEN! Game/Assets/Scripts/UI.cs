using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UI : MonoBehaviour {
    
    public Text timerText;
	
	// Update is called once per frame
	void Update () {
        timerText.text = "Timer: " + GameManager.Instance.timer.ToString("d0");
	}
}
