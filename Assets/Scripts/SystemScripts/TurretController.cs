using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;


public class TurretController : Engineer {

    public GameObject pBullet;

    float timer = 1f;
    public float cooldownLimit = 1f;
    List<Transform> bulletSpawns = new List<Transform>();

    // Use this for initialization
    void Start() {
        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = transform.GetChild(i);
            if (child.name == "BulletSpawnPoint") {
                bulletSpawns.Add(child);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (eController.Action1.WasPressed && timer > cooldownLimit) {
            foreach (Transform pos in bulletSpawns) {
                GameObject bullet = Instantiate(pBullet);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.position = pos.position;
            }
            timer = 0f;
        }

        transform.RotateAround(transform.position, transform.up, eController.LeftStickX.Value);
		print (eController.LeftStickX.Value);

        timer += Time.deltaTime;
    }
}
