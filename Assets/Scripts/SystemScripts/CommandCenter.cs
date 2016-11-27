using UnityEngine;
using System.Collections;

public class CommandCenter : ShipSystem {
    const int commandHealth = 3;
	public Material brokenMat;
	Material normalMat;
	MeshRenderer mr;
    protected override void Start() {
        base.Start();
        health = commandHealth;

		mr = GetComponent<MeshRenderer> ();
		normalMat = mr.material;
    }

    protected override void Break() {
        base.Break();
        sc.BreakCommandCenter();
		mr.material = brokenMat;
    }
    protected override void ResetHealth() {
        health = commandHealth;
        sc.FixedCommandCeneter();
		mr.material = normalMat;
    }
}
