using UnityEngine;
using System.Collections;

public class TurretInit : MonoBehaviour {

    Turrets turretScript;
    TurretController tc;
    Camera cam;
	// Use this for initialization
	void Start () {
        turretScript = GetComponent<Turrets>();
        tc = GetComponent<TurretController>();
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Engineer") {
            turretScript.ec = col.gameObject.GetComponent<EngineerController>();

            tc.horizontal = "E_HL" + turretScript.ec.teamNum;
            tc.vertical ="E_VL" + turretScript.ec.teamNum;
            tc.shootButton = "joystick " + turretScript.joystickNum + " button 0";
			print (tc.shootButton);
            cam.rect = col.gameObject.GetComponentInChildren<Camera>().rect;
            Destroy(this);
        }
    }
}
