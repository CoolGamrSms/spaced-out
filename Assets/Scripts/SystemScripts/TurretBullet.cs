using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {
    public float speed = 1f;
    public float timer = 10f;
    [HideInInspector] public bool bounce = false;

    void Start() {
        //GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0) Destroy(gameObject);
    }
    void OnTriggerEnter(Collider col) {
            if(col.gameObject.tag != "Ship1" && col.gameObject.tag != "Ship2") Destroy(gameObject);
    }
}
