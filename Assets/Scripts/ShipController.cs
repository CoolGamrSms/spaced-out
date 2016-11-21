using UnityEngine;
using System.Collections;
using InControl;

public class ShipController : MonoBehaviour {

    public int playerNumber;
    InputDevice sController;

    public float speed;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVel;

    private Vector3 moveDir;
    private Vector3 lookDir;
    Vector3 vel;

    void Awake() {
        sController = PlayerInputManager.Instance.controllers[playerNumber];
    }

    void Update() {
        if (sController.Action1.WasPressed) {
            Fire();
        }
    }

    void FixedUpdate() {
        vel = new Vector3(sController.LeftStickX.Value * speed, sController.LeftStickY.Value * speed, speed * 0.5f);

        transform.position += vel * Time.deltaTime;

        transform.rotation = Quaternion.Euler((sController.LeftStickY.Value * 5) - 90, (sController.LeftStickX.Value * 5) - 90, 0);
    }

    void Fire() {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVel, ForceMode.Impulse);

        Destroy(bullet, 2.0f);
    }

    public void HullBreach() {
        speed *= .9f;
    }

    public void FixBreach() {
        speed *= 1.1f;
    }
}
