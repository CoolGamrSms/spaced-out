using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
    const int hullHealth = 1;
    protected override void Start() {
        base.Start();
        health = hullHealth;
    }

    protected override void ResetHealth() {
        health = hullHealth;
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (!broken) {
            gameObject.SetActive(false);
        }
    }
}
