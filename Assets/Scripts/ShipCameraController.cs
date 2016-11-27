using UnityEngine;
using System.Collections;

public class ShipCameraController : MonoBehaviour {

    public float lerpSpeed;
    public Vector3 followOffset;
    public GameObject ship;
    private float lerpActualSpeed;

    void Start() {
        lerpActualSpeed = 0f;
    }

    void FixedUpdate () {
        lerpActualSpeed += Time.deltaTime;
        lerpActualSpeed = Mathf.Min(lerpActualSpeed, lerpSpeed);
        transform.position = Vector3.Lerp(transform.position, ship.transform.position + ship.transform.forward * -10f + ship.transform.up*3f, Time.deltaTime * lerpActualSpeed);
        transform.LookAt(ship.transform);
    }
}
