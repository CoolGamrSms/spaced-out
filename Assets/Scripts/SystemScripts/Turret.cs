using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;


public class Turret : ShipSystem {
    //Set in TurretInit
    public EngineerController ec;
    public TurretController tc;
    public Camera cam;

    bool turretEngaged;

    // Use this for initialization
    protected override void Start() {
        base.Start();
        cam.rect = ec.gameObject.GetComponentInChildren<Camera>().rect;
        tc.enabled = false;
        cam.enabled = false;
        turretEngaged = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (interacting && eController.Action1.WasPressed) {
            if (!turretEngaged) {
                turretEngaged = true;
                ec.enabled = false;
                tc.enabled = true;
                cam.enabled = true;
            }
        }
        if(eController.Action2.WasPressed)
        {
            if (turretEngaged)
            {
                ec.enabled = true;
                tc.enabled = false;
                cam.enabled = false;
                turretEngaged = false;
            }
        }
    }
}
