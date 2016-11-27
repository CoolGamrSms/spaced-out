using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;


public class TurretController : Engineer {

    public GameObject pBullet;

    float timer = 1f;
    public float cooldownLimit = 1f;
    public Transform tilt;
    public float turnRate;
    public float tiltRate;
    List<Transform> bulletSpawns = new List<Transform>();
    ShipController sc;

    // Use this for initialization
    void Start() {
        sc = GetComponentInParent<ShipController>();
        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = tilt.GetChild(i);
            if (child.name == "BulletSpawnPoint") {
                bulletSpawns.Add(child);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (eController.Action1.IsPressed && timer > cooldownLimit) {
            foreach (Transform pos in bulletSpawns) {
                GameObject bullet = Instantiate(pBullet);
                bullet.transform.rotation = pos.rotation;
                bullet.transform.position = pos.position;
            }
            timer = 0f;
        }

        if (sc.commandCenterBroken) return;
        transform.RotateAround(transform.position, transform.up, eController.LeftStickX.Value*turnRate);
        tilt.transform.RotateAround(tilt.transform.position, tilt.transform.right, -eController.LeftStickY.Value*tiltRate);
		//print (eController.LeftStickX.Value);

        timer += Time.deltaTime;
    }
}
