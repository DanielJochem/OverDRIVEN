using UnityEngine;
using System.Collections;

public class RobotController : MonoBehaviour {

    //Get the transform of the robot.
    Transform robot;

    //Set the speed that will be used in the movement and rotation calculations.
    int speed = 30;

    //Set up the Raycast.
    RaycastHit hit;

    //The laser reference
    public LineRenderer laser;

    //Make a Style for the GUI component.
    public GUIStyle myStyle;

    //How long the laser has been on the car, after 5 seconds, signifying that they want to choose that one.
    float selectionTime = 3.0f;

	void Start () {
        //Set the robot transform to the variable.
        robot = this.transform;

        //Setting GameRestarted to false.
        GameManager.Instance.gameRestart = false;
    }

    void Update() {
        //Movement.
        robot.position += Input.GetAxis("Vertical") * -transform.right / (speed * 3);

        //Rotation.
        robot.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * speed * 2);

        //If Left Click is held down,
        if(Input.GetMouseButton(0)) {
            //Show the laser
            laser.enabled = true;
        } else {
            //If left click is let go, turn off the laser.
            laser.enabled = false;
        }

        //Cast the Raycast
        CastRay();        
    }

    void CastRay() {
        if(Physics.Raycast(robot.position, -transform.right, out hit, 1.3f) && laser.enabled) {
            //If the Raycast hits a car,
            if(hit.transform.tag == "Car") {
                //Start counting down to 0 form 5.
                selectionTime -= Time.deltaTime;
                //If the countdown reaches 0,
                if(selectionTime <= 0.0f) {
                    //What car did you laser down? Set it to the selected car.
                    GameManager.Instance.carSelected = hit.transform.name;
                    Debug.Log("Selected: " + hit.transform.name);

                    //Now the game can begin as you have selected a car!
                    GameManager.Instance.gameCanBegin = true;

                    //Load the Game Scene.
                    MenuController.Instance.LoadGameLevel();
                }
            } else {
                //If you moved your laser before the time was up, reset the countdown time.
                selectionTime = 3.0f;
            }

        //If the Raycast is hitting nothing,
        } else {
            //Reset the countdown timer.
            selectionTime = 3.0f;
        }
    }

    void OnGUI() {
        //Display the countdown when it is counting down.
        if(selectionTime < 3.0f && selectionTime > -0.01f)

        //The Style for the GUI Text.
        myStyle = new GUIStyle(GUI.skin.GetStyle("label"));

        //Change the font size and colour of the GUI text.
        myStyle.fontSize = 18;
        myStyle.normal.textColor = Color.yellow;

        GUI.Label(new Rect(Screen.width / 2.5f, 20, 290, 30), "Selecting " + hit.transform.name + " in: " + selectionTime.ToString("F2") + " seconds", myStyle);
    }
}
