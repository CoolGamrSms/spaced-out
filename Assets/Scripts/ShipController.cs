using UnityEngine;
using System.Collections;
using InControl;

public class ShipController : MonoBehaviour {

    public int playerNumber;
    InputDevice sController;

    public float speed;
	float maxSpeed;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVel;
    public float rollBack;
    public float rollAngle;
    public float turnSpeed;

    private Vector3 moveDir;
    private Vector3 lookDir;
    private Rigidbody rb;
    Vector3 vel;

    void Awake() {
        sController = PlayerInputManager.Instance.controllers[playerNumber];
    }

    void Start () {
		maxSpeed = speed;
        rb = GetComponent<Rigidbody>();

    }

    void Update() {
        if (sController.Action1.WasPressed) {
            Fire();
        }
    }


    void FixedUpdate()
    {
        rb.AddRelativeTorque (sController.LeftStickY.Value * turnSpeed,0, 0); // W key or the up arrow to turn upwards, S or the down arrow to turn downwards. 
        rb.AddRelativeTorque (0, sController.LeftStickX.Value * turnSpeed,0); // A or left arrow to turn left, D or right arrow to turn right. 
        rb.AddRelativeForce(transform.forward * speed);

        Quaternion q = transform.rotation;
        q = Quaternion.Euler(q.eulerAngles.x, q.eulerAngles.y, -sController.LeftStickX.Value * rollAngle);
        Quaternion rot = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rollBack);
        transform.rotation = rot;
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

	public void BreakEngine(){
		speed = 0f;
	}

	public void FixEngine(){
		speed = maxSpeed;
	}
}
