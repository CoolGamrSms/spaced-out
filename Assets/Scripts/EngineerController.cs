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
    GameObject interaction;
    bool gravity = true;
    float gravityValue = 0f;

    void Start() {

    }

    void Update() {
        CharacterController cc = GetComponent<CharacterController>();
        transform.RotateAround(transform.position, Vector3.up, eController.RightStickX.Value * lookSpeed);
        transform.Rotate(-eController.RightStickY.Value * lookSpeed, 0, 0);
        if (cc.isGrounded || !gravity) gravityValue = 0f;
        else gravityValue -= 9.8f * Time.deltaTime;

        Vector3 speed;

        if (gravity) speed = eController.LeftStickX.Value * transform.right * strafeSpeed 
                           + eController.LeftStickY.Value * Vector3.ProjectOnPlane(transform.forward, Vector3.up) * moveSpeed 
                           + Vector3.up * gravityValue;

        else         speed = eController.LeftStickX.Value * transform.right * strafeSpeed
                           + eController.LeftStickY.Value * transform.forward * moveSpeed
                           + Vector3.up * gravityValue;

        float xRot = transform.eulerAngles.x;
        xRot -= (xRot > 35) ? 360f : 0f; // Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
        cc.Move(speed * Time.deltaTime);
    }

    public void LoseGravity() {
        Debug.Log("Gravity lost");
        moveSpeed *= .5f;
        strafeSpeed *= .5f;
        gravity = false;
    }

    public void ResumeGravity() {
        moveSpeed *= 2f;
        strafeSpeed *= 2f;
        gravity = true;
    }
}
//Mass:1 Drag:10 Speed:1.5
//float strafe = Input.GetAxisRaw("Horizontal") * speed;
//float translation = Input.GetAxisRaw("Vertical") * speed;
//rb.AddRelativeForce(strafe, 0, translation, ForceMode.VelocityChange);
