using UnityEngine;
using System.Collections;
using InControl;

public class EngineerController : Engineer {
    // Movement
    public float moveSpeed = 1f;
    public float strafeSpeed = 1f;
    public float lookSpeed = 1f;

    private Vector3 moveDir;
    private Vector3 lookDir;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        moveDir = eController.LeftStickX.Value * transform.right * strafeSpeed + eController.LeftStickY.Value * transform.forward * moveSpeed;
        rb.velocity = moveDir;

        lookDir = eController.RightStickX.Value * transform.up;
        rb.angularVelocity = lookDir * lookSpeed;
    }

    public void LoseGravity() {
        rb.useGravity = false;
        moveSpeed *= .5f;
        strafeSpeed *= .5f;
    }

    public void ResumeGravity() {
        rb.useGravity = true;
        moveSpeed *= 2f;
        strafeSpeed *= 2f;
    }
}
//Mass:1 Drag:10 Speed:1.5
//float strafe = Input.GetAxisRaw("Horizontal") * speed;
//float translation = Input.GetAxisRaw("Vertical") * speed;
//rb.AddRelativeForce(strafe, 0, translation, ForceMode.VelocityChange);
