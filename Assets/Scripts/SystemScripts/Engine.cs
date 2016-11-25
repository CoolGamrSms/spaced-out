using UnityEngine;
using System.Collections;

public class Engine : ShipSystem {
    const int engineHealth = 3;
	ShipController sc;
	ParticleSystem particles;

    protected override void Start() {
        base.Start();
        health = engineHealth;
		sc = GetComponentInParent<ShipController> ();
		particles = GetComponentInChildren<ParticleSystem> ();
    }

	protected override void Break ()
	{
		base.Break ();
		sc.BreakEngine ();
		particles.Stop ();

	}

    protected override void ResetHealth() {
		sc.FixEngine ();
        health = engineHealth;
		particles.Play ();
    }
}
