using UnityEngine;
using System.Collections;

public class CommandCenter : ShipSystem {
    ShipController ship;
    const int commandHealth = 3;

    protected override void Start() {
        base.Start();
        int teamNumber = 0;
        if (playerNumber == 0 || playerNumber == 1)
            teamNumber = 1;
        else
            teamNumber = 2;
        ship = GameObject.FindGameObjectWithTag(
            "Ship" + teamNumber.ToString()).GetComponent<ShipController>();
        health = commandHealth;
    }

    protected override void Break() {
        base.Break();
        ship.BreakCommandCenter();
    }
    protected override void ResetHealth() {
        health = commandHealth;
        ship.FixedCommandCeneter();
    }
}
