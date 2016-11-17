using UnityEngine;
using System.Collections;

public class Turret : ShipSystem {

	public EngineerController ec;
	public TurretController tc;
	public Camera cam;

    string startButton;
    string backButton;

    // Use this for initialization
    protected override void Start() {
        base.Start();

        startButton = "joystick " + joystickNum + " button 0";
        backButton = "joystick " + joystickNum + " button 1";

		tc.horizontal = "E_HL" + ec.teamNum;
		tc.vertical = "E_VL" + ec.teamNum;
		tc.shootButton = "joystick " + joystickNum + " button 0";

		cam.rect = ec.gameObject.GetComponentInChildren<Camera>().rect;

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
