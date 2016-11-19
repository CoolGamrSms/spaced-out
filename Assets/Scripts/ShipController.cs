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

    void Start () {
		horiontalLeft += teamNum;
		horizontalRight += teamNum;
		verticalLeft += teamNum;
		verticalRight += teamNum;
		fire += teamNum;

        //Team 1's engineer joystick is 1, while team 2 engineer joystick is 4
        //Sets the interactable objects to react to correct joystick input
        foreach (ShipSystem ob in GetComponentsInChildren<ShipSystem>()) {
			if (teamNum == 1) {
				ob.joystickNum = 1;
			} else {
				ob.joystickNum = 4;
			}
        }
    }

    void Update() {
        if (Input.GetButtonDown(fire)) {
            Fire();
        }
    }

    void FixedUpdate() {
		vel = new Vector3(Input.GetAxis(horiontalLeft) * speed, Input.GetAxis(verticalLeft) * speed, speed * 0.5f);

		transform.position += vel * Time.deltaTime;

		transform.rotation = Quaternion.Euler((Input.GetAxis(verticalLeft) * 5) -90, (Input.GetAxis(horiontalLeft) * 5) -90, 0);
    }

    void Fire() {
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletVel, ForceMode.Impulse);

		Destroy(bullet, 2.0f);
    }
}
