﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pickup_Controller : MonoBehaviour {

    //Variables for text
    public Text UIfirmware;
	private int speedFIRM = 0;
    private int armorFIRM = 0;
    private int hackerFIRM = 0;
    private int turningFIRM = 0;
    public GameObject Car;

    // Use this for initialization
    void Start () {

        Car = GameObject.FindGameObjectWithTag("Car");
        

    }
	
	// Update is called once per frame
	void Update () {
        //setting the UI to current values
        UIfirmware.text = ("Speed X " + speedFIRM + ", Armor X " + armorFIRM + ", Handling X " + turningFIRM + ", Hacker Slowdown X " + hackerFIRM);


        //Getting the current variables for the pickups
        speedFIRM = Car.GetComponent<PlayerController>().speedFIRM;
        armorFIRM = Car.GetComponent<PlayerController>().armorFIRM;
        hackerFIRM = Car.GetComponent<PlayerController>().hackerFIRM;
        turningFIRM = Car.GetComponent<PlayerController>().turningFIRM;
    }

    
}