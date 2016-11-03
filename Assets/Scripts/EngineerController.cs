using UnityEngine;
using System.Collections;

public class EngineerController : MonoBehaviour {


    //Movement
    public int teamNum = 0;
    public float moveSpeed = 1f;
    public float lookSpeed = 1f;

    private Vector3 moveDir;
    private Vector3 lookDir;
    private Rigidbody rb;
    private string horiontalLeft = "E_HL";
    private string horizontalRight = "E_HR";
    private string verticalLeft = "E_VL";
    private string verticalRight = "E_VR";

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        horiontalLeft += teamNum;
        horizontalRight += teamNum;
        verticalLeft += teamNum;
        verticalRight += teamNum;

        transform.GetChild(0).GetComponent<EngineerCameraController>().verticalRight = verticalRight;
	}
	
	// Update is called once per frame
	void Update () {
        moveDir = Input.GetAxis(horiontalLeft) * transform.right + Input.GetAxis(verticalLeft) * transform.forward;
        rb.velocity = moveDir * moveSpeed;

        lookDir = Input.GetAxis(horizontalRight) * transform.up;
        rb.angularVelocity = lookDir * lookSpeed;
	}
}
