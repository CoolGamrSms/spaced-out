using UnityEngine;
using InControl;
using System.Collections;

public class EngineerCameraController : Engineer {
    public float lookSpeed = 1f;
    public string verticalRight;
    float xRot;

    void FixedUpdate() {
        transform.RotateAround(transform.position, transform.right, -eController.RightStickY.Value * lookSpeed);
        xRot = transform.eulerAngles.x;
        xRot -= (xRot > 35) ? 360f : 0f; // Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
