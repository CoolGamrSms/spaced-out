using UnityEngine;
using System.Collections;

public class TurretInit : MonoBehaviour {

    Turret turretScript;
    public TurretController tc;
    public Camera cam;
    // Use this for initialization
    void Start() {
        turretScript = GetComponent<Turret>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Engineer") {
            turretScript.ec = col.gameObject.GetComponent<EngineerController>();

            tc.horizontal = "E_HL" + turretScript.ec.teamNum;
            tc.vertical = "E_VL" + turretScript.ec.teamNum;
            tc.shootButton = "joystick " + turretScript.joystickNum + " button 0";
           	
			turretScript.tc = tc;

            cam.rect = col.gameObject.GetComponentInChildren<Camera>().rect;
			turretScript.cam = cam;

            Destroy(this);
        }
    }
}
