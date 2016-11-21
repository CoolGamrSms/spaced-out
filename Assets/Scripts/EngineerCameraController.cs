using UnityEngine;
using InControl;
using System.Collections;

public class EngineerCameraController : Engineer {
    public float lookSpeed = 1f;
    public string verticalRight;
    float xRot;

    void Start() {
    }

    void FixedUpdate() {
        transform.RotateAround(transform.position, transform.right, -eController.RightStickY.Value * lookSpeed);
        xRot = transform.eulerAngles.x;
        xRot -= (xRot > 35) ? 360f : 0f; // Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

//public class EngineerCameraController : Engineer {
//    Vector2 mouseLook;
//    Vector2 smoothV;
//    public float sensitivity = 2.0f;
//    public float smoothing = 3.0f;
//    GameObject character;

//    void Start() {
//        character = transform.parent.gameObject;
//    }

//    void FixedUpdate() {
//        Vector2 mouseDiretion = new Vector2(eController.RightStickX, eController.RightStickY);
//        mouseDiretion = Vector2.Scale(
//            mouseDiretion, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
//        smoothV.x = Mathf.Lerp(smoothV.x, mouseDiretion.x, 1f / smoothing);
//        smoothV.y = Mathf.Lerp(smoothV.y, mouseDiretion.y, 1f / smoothing);
//        mouseLook += smoothV;
//        mouseLook.y = Mathf.Clamp(mouseLook.y, -45f, 45f);
//        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
//        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
//    }
//}
