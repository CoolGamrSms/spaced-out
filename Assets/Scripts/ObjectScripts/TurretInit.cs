using UnityEngine;
using System.Collections;

public class TurretInit : MonoBehaviour {

    Turrets turretScript;
    Camera cam;
	// Use this for initialization
	void Start () {
        turretScript = GetComponent<Turrets>();
        cam = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Engineer") {
            turretScript.ec = col.gameObject.GetComponent<EngineerController>();
            cam.rect = col.gameObject.GetComponentInChildren<Camera>().rect;
            Destroy(this);
        }
    }
}
