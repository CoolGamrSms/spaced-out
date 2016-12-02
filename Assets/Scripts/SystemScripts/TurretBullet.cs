using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {
    public float speed = 1f;
    public float timer = 4f;
    [HideInInspector] public bool bounce = false;
    [HideInInspector]
    public Vector3 shipV;

    void Start() {
        GetComponent<Rigidbody>().velocity = shipV + transform.forward * speed;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col) {
            if(col.gameObject.tag != "Ship1" && col.gameObject.tag != "Ship2" && col.gameObject.tag!="Ring") Destroy(gameObject);
    }
}
