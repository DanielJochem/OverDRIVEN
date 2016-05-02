using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : SingletonBehaviour<GameManager> {

    //Car is selected, game can now begin
    /*@*@*@* Add when you crash, set to false *@*@*@*/
    public bool gameCanBegin;
    
    //Game is over, go back to car selection screen.
    /*@*@*@* Add when you crash, set to true *@*@*@*/
    public bool gameRestarted;

    //The hacker timer.
    public float timer;
    public float timerPERCENT = 30;

    //Cars to choose from.
    public string carSelected;

    //make dead
    public bool dead;

    //The 4 directions you can move in.
    public string forwardC,
                  backwardC,
                  leftC,
                  rightC;

    //The different keys that the Failsecure system can change your controls to.
    public string[] arrayOfKeys = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};


    void Start () {
        //Initial set controls to WASD.
        ResetControls();

        Object.DontDestroyOnLoad(this.gameObject);
    }

    void Update() {
        if(gameCanBegin && SceneManager.GetActiveScene().name == "OverDRIVEN Game Scene") {
            timer += Time.deltaTime;

            //If the counter is at a 30 second mark,
            if(timer % timerPERCENT <= 0.02f && timer > 1f) {
                //Change the controls.
                HackerControls();
            }

            if(gameRestarted)
            {
                RestartGame();
            }
        }
    }
    
    //When reassigning controls,
    int GetRandResult() {
        //Grab a random letter from the array,
        int result = Random.Range(0, arrayOfKeys.Length);

        //and return it.
        return result;
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
        dead = false;
        SceneManager.LoadScene(1);

    }
}
