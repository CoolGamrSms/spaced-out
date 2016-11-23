using UnityEngine;
using System.Collections;

public class GravityGenerator : ShipSystem {
    
	public EngineerController ec;
	Rigidbody rb;

	const int generatorHealth = 3;
    protected override void Start() {
        rb = GetComponent<Rigidbody>();
        base.Start();
        health = generatorHealth;
    }

    protected override void ResetHealth() {
		rb.useGravity = true;
		enabled = false;
		ec.ResumeGravity ();
        health = generatorHealth;
    }

	protected override void Break ()
	{
		base.Break ();
		transform.position += new Vector3 (0f, 1f, 0f);
		rb.useGravity = false;
		ec.LoseGravity ();
	}
}
