using UnityEngine;
using System.Collections;

public class CommandCenter : ShipSystem {

    const int commandHealth = 3;
    protected override void Start() {
        base.Start();
        health = commandHealth;
    }

    protected override void ResetHealth() {
        health = commandHealth;
    }
}
