using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager> {

    //Car is selected, game can now begin
    public bool gameCanBegin;

    //Game is over, go back to car selection screen.
    public bool gameRestart;

    //The hacker timer.
    public float timer;
    public float timerPERCENT;

    //Cars to choose from.
    public string carSelected;

    //make dead
    public bool isDead;

    //Turning taillights on and off
    public bool tailLightsOn;
    public GameObject leftTailLight;
    public GameObject rightTailLight;
    public Material redTailLight;
    public Material greyTailLight;

    //The 4 directions you can move in.
    public string forwardC,
                  backwardC,
                  leftC,
                  rightC;

    public Text controlsText;

    //The different keys that the Failsecure system can change your controls to.
    public string[] arrayOfKeys = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


    void Start() {
        //Initial set controls to WASD.
        ResetControls();

        Object.DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        if(gameCanBegin && SceneManager.GetActiveScene().name == "Game Scene") {
            timer += Time.deltaTime;

            if(controlsText == null && GameObject.Find("Controls").GetComponent<Text>() != null) {
                controlsText = GameObject.Find("Controls").GetComponent<Text>();
            }

            LightsCheck();

            //If the counter is at a 30 second mark,
            if(timer % timerPERCENT <= 0.02f && timer > 1f) {
                //Change the controls.
                HackerControls();
                controlsText.fontSize = 300;
            }

            if(controlsText.fontSize > 180) {
                controlsText.fontSize -= 1;
            }

            if(gameRestart)
                RestartGame();
        }
    }

    //When reassigning controls,
    int GetRandResult() {
        //Grab a random letter from the array,
        int result = Random.Range(0, arrayOfKeys.Length);

        //and return it.
        return result;
    }

    void LightsCheck() {
        if(GameObject.FindGameObjectWithTag("LTL") != null && leftTailLight == null) {
            leftTailLight = GameObject.FindGameObjectWithTag("LTL");
            rightTailLight = GameObject.FindGameObjectWithTag("RTL");
        }

        if((timer % (timerPERCENT - 3.0f) <= 0.02f || timer % (timerPERCENT - 2.0f) <= 0.02f || timer % (timerPERCENT - 1.0f) <= 0.02f) && timer > 1f) {
            tailLightsOn = true;
            Debug.Log("Lights on");
        }

        if((timer % (timerPERCENT - 2.5f) <= 0.02f || timer % (timerPERCENT - 1.5f) <= 0.02f || timer % (timerPERCENT - 0.5f) <= 0.02f) && timer > 1f) {
            tailLightsOn = false;
            Debug.Log("Lights off");
        }

        if(timer % timerPERCENT >= 2.0f && timer > timerPERCENT) {
            tailLightsOn = false;
            Debug.Log("Lights off after");
        } else if(timer % timerPERCENT <= 0.02f && timer % timerPERCENT < 2 && timer > 1f) {
            tailLightsOn = true;
            Debug.Log("Lights on after");
        }


        if(tailLightsOn) {
            leftTailLight.gameObject.GetComponent<Renderer>().sharedMaterial = redTailLight;
            rightTailLight.gameObject.GetComponent<Renderer>().sharedMaterial = redTailLight;
        } else {
            leftTailLight.gameObject.GetComponent<Renderer>().sharedMaterial = greyTailLight;
            rightTailLight.gameObject.GetComponent<Renderer>().sharedMaterial = greyTailLight;
        }
    }

    void HackerControls() {
        //Setting the controls to the same value, so that the while loop can begin.
        forwardC = backwardC = leftC = rightC = ">:)";

        //If any of the directions are assigned the same letter as another,
        while(forwardC == backwardC || forwardC == leftC || forwardC == rightC || backwardC == leftC || backwardC == rightC || leftC == rightC) {
            //Reassign all letters till they are all different from each other.
            forwardC = arrayOfKeys[GetRandResult()];
            backwardC = arrayOfKeys[GetRandResult()];
            leftC = arrayOfKeys[GetRandResult()];
            rightC = arrayOfKeys[GetRandResult()];
            Debug.Log("Forward: " + forwardC + ", Backward: " + backwardC + ", Left: " + leftC + ", Right: " + rightC);
        }
    }

    void ResetControls() {
        forwardC = "w";
        backwardC = "s";
        leftC = "a";
        rightC = "d";
    }

    void RestartGame() {
        ResetControls();
        carSelected = "";
        isDead = false;
        timer = 0.0f;
        MenuController.Instance.LoadGarageLevel();
    }
}