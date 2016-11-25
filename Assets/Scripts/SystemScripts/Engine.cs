using UnityEngine;
using System.Collections;

public class Engine : ShipSystem {
    const int engineHealth = 3;
	ShipController sc;

    protected override void Start() {
        base.Start();
        health = engineHealth;
		sc = GetComponentInParent<ShipController> ();
    }

	protected override void Break ()
	{
		base.Break ();
		//turn off particle system? visual indication of broken
		sc.BreakEngine ();
	}

    protected override void ResetHealth() {
		sc.FixEngine ();
        health = engineHealth;
    }
}
