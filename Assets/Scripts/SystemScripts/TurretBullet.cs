using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {
    public float speed = 1f;

    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag == "Asteroid" || col.gameObject.tag == "Earth") {
            Destroy(gameObject);
        }
    }
}
