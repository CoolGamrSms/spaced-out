using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretController : MonoBehaviour {

    public GameObject pBullet;

    float timer = 1f;
    public float cooldownLimit = 1f;

    public string horizontal;
    public string vertical;
    public string shootButton;
    List<Transform> bulletSpawns = new List<Transform>();

	// Use this for initialization
	void Start () {
        for (int i = 0; i < transform.childCount; ++i) {
            Transform child = transform.GetChild(i);
            if (child.name == "BulletSpawnPoint") {
                bulletSpawns.Add(child);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(shootButton) && timer > cooldownLimit) {
            foreach (Transform pos in bulletSpawns) { 
                GameObject bullet = Instantiate(pBullet);
                bullet.transform.rotation = transform.rotation;
                bullet.transform.position = pos.position;
            }

            timer = 0f;
        }

        transform.RotateAround(transform.position, transform.up, Input.GetAxis(horizontal));

        timer += Time.deltaTime;
	}
}
