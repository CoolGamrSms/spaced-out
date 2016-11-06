using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

    public float speed;
    Vector3 vel;

	private Rigidbody rb;

    void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        vel = new Vector3(Input.GetAxis("Horizontal") * speed, 0, Input.GetAxis("Vertical") * speed);
        
        if (GetArrowInput() && (vel != Vector3.zero)) {
            transform.rotation = Quaternion.LookRotation(vel);
        }
        else {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        rb.velocity = new Vector3(vel.x, rb.velocity.y, vel.z);
    }

    bool GetArrowInput() {
        return (Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0);
    }
}
