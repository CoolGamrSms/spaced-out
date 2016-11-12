using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public int teamNum = 0;
	public float speed;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public float bulletVel;

	private Vector3 moveDir;
	private Vector3 lookDir;
	private string horiontalLeft = "P_HL";
	private string horizontalRight = "P_HR";
	private string verticalLeft = "P_VL";
	private string verticalRight = "P_VR";
	private string fire = "Fire";
    Vector3 vel;

	private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();

		horiontalLeft += teamNum;
		horizontalRight += teamNum;
		verticalLeft += teamNum;
		verticalRight += teamNum;
		fire += teamNum;

        //Team 1's engineer joystick is 1, while team 2 engineer joystick is 4
        //Sets the interactable objects to react to correct joystick input
        foreach (ObjectBase ob in GetComponentsInChildren<ObjectBase>()) {
            ob.joystickNum = 2 * teamNum;
        }
    }

    void Update() {
        if (Input.GetButtonDown(fire)) {
            Fire();
        }
    }

    void FixedUpdate() {
        vel = new Vector3(Input.GetAxis(horiontalLeft) * speed, 0, Input.GetAxis(verticalLeft) * speed);
        
        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }

    bool GetArrowInput() {
        return (Input.GetAxis(horiontalLeft) != 0) || (Input.GetAxis(verticalLeft) != 0);
    }

    void Fire() {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletVel;

        // destroy bullet
    }
}
