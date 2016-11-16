using UnityEngine;
using System.Collections;

public class Engine : ShipSystem {
    const int engineHealth = 3;
    protected override void Start() {
        base.Start();
        health = engineHealth;
    }

    protected override void ResetHealth() {
        health = engineHealth;
    }
}
