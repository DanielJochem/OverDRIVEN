using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Transform car;
    public float moveSpeed = 0.0f;
    public float maxSpeed = 6.66f;
    public float rotateSpeed = 100.0f;

    Vector3 startingPos;
    public GameObject carExplotion;

    public GameObject baseAI,
            armoured,
            supra,
            wrx,
            chevelle,
            gtr,
            stingray;

    public bool doneReset;

    // Use this for initialization
    void Start () {
        car = this.transform;
        startingPos = car.position;
        //I hope Start() runs again once the scene has been loaded for a second time.
        ActivateCar();
    }

    // Update is called once per frame
    void Update() {
        Movement();
    }

    void Movement() { 
        if (!GameManager.Instance.isDead) {
            car.position += transform.forward * moveSpeed * Time.deltaTime;
            //Movement Up and Down
            if (Input.GetKey(GameManager.Instance.forwardC) && Input.GetKey(GameManager.Instance.backwardC)) {
                if (moveSpeed > 0.05f) {
                    moveSpeed -= Time.deltaTime * 3;
                } else if (moveSpeed < 0.05f) {
                    moveSpeed += Time.deltaTime * 3;
                }
                else if (moveSpeed > -0.05f && moveSpeed < 0.05f) {
                    //MASSIVE SKIDDIES
                }

            } else if (Input.GetKey(GameManager.Instance.forwardC)) {
                if (moveSpeed < maxSpeed) {
                    if (moveSpeed < 0.0f) {
                        moveSpeed += Time.deltaTime * 3;
                    } else {
                        moveSpeed += Time.deltaTime;
                    }
                }
            }
            else if (Input.GetKey(GameManager.Instance.backwardC)) {
                if (moveSpeed > -maxSpeed) {
                    if (moveSpeed > 0.0f) {
                        moveSpeed -= Time.deltaTime * 3;
                    } else {
                        moveSpeed -= Time.deltaTime;
                    }
                }

            } else if (moveSpeed > 0.0f) {
                moveSpeed -= Time.deltaTime * 2;

            } else if (moveSpeed < 0.0f) {
                moveSpeed += Time.deltaTime * 2;
            }

            if (Input.GetKey(GameManager.Instance.leftC)) {
                car.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * moveSpeed * 15);
            } else if (Input.GetKey(GameManager.Instance.rightC)) {
                car.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed * 15);
            }

            this.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * moveSpeed / 2;
        }
    }

    void ActivateCar() {
        switch(GameManager.Instance.carSelected) {
            case "Base AI":
                baseAI.gameObject.SetActive(true);
                break;
            case "Armoured":
                armoured.gameObject.SetActive(true);
                break;
            case "Supra":
                supra.gameObject.SetActive(true);
                break;
            case "WRX":
                wrx.gameObject.SetActive(true);
                break;
            case "Chevelle":
                chevelle.gameObject.SetActive(true);
                break;
            case "GTR":
                gtr.gameObject.SetActive(true);
                break;
            case "Stingray":
                stingray.gameObject.SetActive(true);
                break;
        }
    }

    void DeactivateCar() {
        for(var i = 0; i < this.transform.GetChild(0).childCount; i++) {
            this.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        GameManager.Instance.carSelected = "";
    }

    IEnumerator WaitForExplosion() {
        yield return new WaitForSeconds(2);
        DeactivateCar();
        car.position = startingPos;
        GameManager.Instance.gameRestart = true;
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Buildings" && (moveSpeed >= maxSpeed / 2 || moveSpeed <= -maxSpeed / 2)) {
            GameManager.Instance.isDead = true;
            Instantiate(carExplotion, car.transform.position, car.transform.rotation);
            StartCoroutine(WaitForExplosion());
        }
    }
}
