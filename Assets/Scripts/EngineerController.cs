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
        CharacterController cc = GetComponent<CharacterController>();
        transform.RotateAround(transform.position, Vector3.up, eController.RightStickX.Value * lookSpeed);
        transform.Rotate(-eController.RightStickY.Value * lookSpeed, 0, 0);
        Vector3 speed = eController.LeftStickX.Value * transform.right * strafeSpeed + eController.LeftStickY.Value * transform.forward * moveSpeed;
        float xRot = transform.eulerAngles.x;
        xRot -= (xRot > 35) ? 360f : 0f; // Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
        cc.SimpleMove(speed);
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
