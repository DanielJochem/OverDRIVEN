using UnityEngine;
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
            lambo,
            stingray;

    public bool doneReset;

    public int speedFIRM = 0;
    public int armorFIRM = 0;
    public int hackerFIRM = 0;
    public int turningFIRM = 0;

    public AudioClip Pickup;
    public AudioSource PickupAudio;

    public GameObject destroyed;
    public GameObject chevelleDestroyed;
    public GameObject supraDestroyed;
    public GameObject otherDestroyed;

    public CameraController camController;
    public Rigidbody rb;

    public float timer = 5.0f;

    //public bool onGround;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody>();
        car = this.transform;
        startingPos = car.position;
        //I hope Start() runs again once the scene has been loaded for a second time.
        ActivateCar();
        

    }

    // Update is called once per frame
    void Update() {
        Movement();

        if(rb.velocity < 0.1f && rb.velocity > -0.1f) {

            if(timer > 0.0f) {
                timer -= Time.deltaTime;
            } else {
                DeactivateCar();
                GameManager.Instance.carSelected = "";
                car.position = startingPos;
                GameManager.Instance.gameRestart = true;
            }
        } else {
            timer = 5.0f;
        }
    }

    void Movement() {
        if (!GameManager.Instance.isDead /*&& onGround*/) {
            rb.position += transform.forward * moveSpeed * Time.deltaTime;
            //Movement Up and Down
            if (Input.GetKey(GameManager.Instance.forwardC) && Input.GetKey(GameManager.Instance.backwardC))
            {
                if (moveSpeed > 0.05f)
                {
                    moveSpeed -= Time.deltaTime * 6;
                }
                else if (moveSpeed < 0.05f)
                {
                    moveSpeed += Time.deltaTime * 6;
                }
                else if (moveSpeed > -0.05f && moveSpeed < 0.05f)
                {
                    //MASSIVE SKIDDIES
                }

            }
            else if (Input.GetKey(GameManager.Instance.forwardC))
            {
                if (moveSpeed < maxSpeed)
                {
                    if (moveSpeed < 0.0f)
                    {
                        moveSpeed += Time.deltaTime * 6;
                    }
                    else
                    {
                        moveSpeed += Time.deltaTime * 2;
                    }
                }

            }
            else if (Input.GetKey(GameManager.Instance.backwardC))
            {
                if (moveSpeed > -maxSpeed)
                {
                    if (moveSpeed > 0.0f)
                    {
                        moveSpeed -= Time.deltaTime * 6;
                    }
                    else
                    {
                        moveSpeed -= Time.deltaTime * 2;
                    }
                }

            }
            else if (moveSpeed > 0.0f)
            {
                moveSpeed -= Time.deltaTime * 4;

            }
            else if (moveSpeed < 0.0f)
            {
                moveSpeed += Time.deltaTime * 4;
            }

            if (Input.GetKey(GameManager.Instance.leftC))
            {
                car.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * (moveSpeed * 8));
            }
            else if (Input.GetKey(GameManager.Instance.rightC))
            {
                car.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * (moveSpeed * 8));
            }
        }
    }

    void ActivateCar()
    {
        switch (GameManager.Instance.carSelected)
        {
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
            case "Lambo":
                lambo.gameObject.SetActive(true);
                break;
            case "Stingray":
                stingray.gameObject.SetActive(true);
                break;
        }
    }

    void DeactivateCar()
    {
        for (var i = 0; i < this.transform.GetChild(0).childCount; i++)
        {
            this.transform.GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.carSelected = "";
        Destroy(destroyed.gameObject);
        destroyed = null;
        car.position = startingPos;
        camController.distance = 0.5f;
        GameManager.Instance.gameRestart = true;
    }

    void OnCollisionEnter(Collision other)
    {
           //onGround = true;

        if(other.gameObject.tag == "Buildings") {
            if(moveSpeed >= maxSpeed / 2 || moveSpeed <= -maxSpeed / 2) {
                if(armorFIRM == 0) {
                    GameManager.Instance.isDead = true;
                    DeactivateCar();
                    camController.distance = 1.3f;
                    Instantiate(carExplotion, car.transform.position, car.transform.rotation);

                    if(GameManager.Instance.carSelected == "Chevelle") {
                        destroyed = (GameObject)Instantiate(chevelleDestroyed, new Vector3(car.transform.position.x - 0.15f, 0.05f, car.transform.position.z), car.transform.rotation);
                    } else if(GameManager.Instance.carSelected == "Supra") {
                        destroyed = (GameObject)Instantiate(supraDestroyed, new Vector3(car.transform.position.x - 0.21f, 0.038f, car.transform.position.z), car.transform.rotation);
                    } else {
                        destroyed = (GameObject)Instantiate(otherDestroyed, new Vector3(car.transform.position.x, 0.4f, car.transform.position.z), car.transform.rotation);
                    }

                    StartCoroutine(WaitForExplosion());
                } else {
                    armorFIRM--;
                }
            }

        } else if(other.gameObject.tag == "OOB") {
            GameManager.Instance.gameRestart = true;

        } else if (other.gameObject.tag != "Floor" && other.gameObject.tag != null) {
            if(other.gameObject.tag == "Wheel_Pickup") {
                turningFIRM++;
                rotateSpeed = rotateSpeed + 10;

            } else if(other.gameObject.tag == "Hacker_Pickup") {
                hackerFIRM++;
                GameManager.Instance.timerPERCENT = GameManager.Instance.timerPERCENT + 15;

            } else if(other.gameObject.tag == "Armor_Pickup") {
                armorFIRM++;

            } else if(other.gameObject.tag == "Speed_Pickup") {
                speedFIRM++;
                maxSpeed = maxSpeed + 1;
            }

            PickupAudio.Play();
            

            Destroy(other.gameObject);
        }
    }

    /*
    void OnCollisionExit(Collision other) {
        if(other.gameObject.tag == "Floor")
            onGround = false;
    } */
}
