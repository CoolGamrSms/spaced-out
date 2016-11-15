using UnityEngine;
using System.Collections;

public class GravityGenerator : ShipSystem {
    const int generatorHealth = 3;
    protected override void Start() {
        base.Start();
        health = generatorHealth;
    }

    protected override void ResetHealth() {
        health = generatorHealth;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!broken) {
            //remove negative effects
            GetComponent<Rigidbody>().useGravity = true;
            enabled = false;
        }
    }
}
