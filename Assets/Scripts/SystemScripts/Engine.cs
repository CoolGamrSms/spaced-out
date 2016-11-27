using UnityEngine;
using System.Collections;

public class Engine : ShipSystem {
    const int engineHealth = 3;
    public Material m2;
    Material m;

    protected override void Start() {
        base.Start();
        health = engineHealth;
        m = GetComponent<Renderer>().material;
    }

	protected override void Break ()
	{
		base.Break ();
		sc.BreakEngine ();
        GetComponent<Renderer>().material = m2;
	}

    protected override void ResetHealth() {
		sc.FixEngine ();
        health = engineHealth;
        GetComponent<Renderer>().material = m;
    }
}
