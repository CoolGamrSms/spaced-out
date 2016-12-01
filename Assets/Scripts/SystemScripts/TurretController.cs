using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class TurretController : Engineer
{
    AudioSource shoot;
    public GameObject pBullet;

    float timer = 1f;
    public float cooldownLimit = 1f;
    public Transform tilt;
    public float turnRate;
    public float tiltRate;
    List<Transform> bulletSpawns = new List<Transform>();
    ShipController sc;
	Rigidbody shipRb;
	float mypitch;

    void Start()
    {
        shoot = GetComponent<AudioSource>();
        sc = GetComponentInParent<ShipController>();
		shipRb = GetComponentInParent<Rigidbody> ();
        mypitch = shoot.pitch;

        for (int i = 0; i < transform.childCount; ++i)
        {
            Transform child = tilt.GetChild(i);

            if (child.name == "BulletSpawnPoint")
            {
                bulletSpawns.Add(child);
            }
        }
    }

    void FixedUpdate()
    {
        if (eController.Action1.IsPressed && timer > cooldownLimit)
        {
            foreach (Transform pos in bulletSpawns)
            {
                shoot.pitch = mypitch + Random.Range(-0.1f, 0f);
                shoot.Play();
                GameObject bullet = Instantiate(pBullet);
                bullet.transform.rotation = pos.rotation;
                bullet.transform.position = pos.position;
                bullet.GetComponent<TurretBullet>().shipV = shipRb.velocity;
            }

            timer = 0f;
        }

        timer += Time.deltaTime;

        if (sc.commandCenterBroken) return;

        transform.RotateAround(transform.position, transform.up, eController.LeftStickX.Value * turnRate);
		float xRot = transform.localRotation.eulerAngles.x;
		xRot -= (xRot > 90) ? 360f : 0f; // Euler angles doesn't like negatives
		xRot = Mathf.Clamp(xRot, -90f, 90f);
		xRot += (xRot < 0) ? 360f : 0f;
		transform.localRotation = Quaternion.Euler(xRot, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);
		Debug.Log (transform.localRotation.eulerAngles.x);

        tilt.transform.RotateAround(tilt.transform.position, tilt.transform.right, -eController.LeftStickY.Value * tiltRate);

    }
}
