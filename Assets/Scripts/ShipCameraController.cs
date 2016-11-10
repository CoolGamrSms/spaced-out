using UnityEngine;
using System.Collections;

public class ShipCameraController : MonoBehaviour {

    public float rotationSpeed;

    void FixedUpdate () {
        transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.transform.rotation, Time.deltaTime * rotationSpeed);
    }
}
