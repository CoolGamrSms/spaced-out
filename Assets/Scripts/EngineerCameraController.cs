using UnityEngine;
using System.Collections;

public class EngineerCameraController : MonoBehaviour {

    public float lookSpeed = 1f;
    public string verticalRight;
    float xRot;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
        transform.RotateAround(transform.position, transform.right,  Input.GetAxis(verticalRight) * lookSpeed);
        xRot = transform.eulerAngles.x;
        xRot -= (xRot > 35) ? 360f: 0f; //Euler angles doesn't like negatives
        xRot = Mathf.Clamp(xRot, -30f, 30f);
        xRot += (xRot < 0) ? 360f : 0f;
        transform.rotation = Quaternion.Euler(xRot, transform.eulerAngles.y, transform.eulerAngles.z);
	}
}
