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

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Engineer")) {
            turretScript.ec = col.gameObject.GetComponent<EngineerController>();
            turretScript.tc = tc;
            cam.rect = col.gameObject.GetComponentInChildren<Camera>().rect;
            turretScript.cam = cam;
            Destroy(this);
        }
    }
}
