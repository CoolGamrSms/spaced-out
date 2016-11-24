using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;


public class Turret : ShipSystem {
    //Set in TurretInit
    public EngineerController ec;
    public TurretController tc;
    public Camera cam;

    bool turretEngaged = false;

    // Use this for initialization
    protected override void Start() {
        health = turretHealth;
        cam.rect = ec.gameObject.GetComponentInChildren<Camera>().rect;
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (eController.Action1.WasPressed) {
            if (!turretEngaged) {
                turretEngaged = true;
                ec.enabled = false;
                tc.enabled = true;
                cam.enabled = true;
            }
            else {
                ec.enabled = true;
                tc.enabled = false;
                cam.enabled = false;
                turretEngaged = false;
            }
        }
    }

    const int turretHealth = 3;
    protected override void ResetHealth() {
        health = turretHealth;
    }
}
