using UnityEngine;
using System.Collections;

public class Turret : ShipSystem {

    string startButton;
    string backButton;

    //Set by TurretInit script
    public EngineerController ec;
    TurretController tc;
    Camera cam;

    // Use this for initialization
    protected override void Start() {
        base.Start();

        startButton = "joystick " + joystickNum + " button 0";
        backButton = "joystick " + joystickNum + " button 1";

        tc = GetComponent<TurretController>();
        cam = GetComponentInChildren<Camera>();
        cam.enabled = false;
        tc.enabled = false;
        health = turretHealth;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(startButton)) {
            ec.enabled = false;
            tc.enabled = true;
            cam.enabled = true;
        }
        else if (Input.GetKeyDown(backButton)) {
            ec.enabled = true;
            tc.enabled = false;
            cam.enabled = false;
        }
    }

    const int turretHealth = 3;
    protected override void ResetHealth() {
        health = turretHealth;
    }
}
