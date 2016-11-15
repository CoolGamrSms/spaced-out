using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {
    public float speed = 10f;
    // Use this for initialization
    void Start() {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter() {
        //Destroy(gameObject);
    }
}
