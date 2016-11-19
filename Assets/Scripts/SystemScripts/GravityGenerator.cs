using UnityEngine;
using System.Collections;

public class GravityGenerator : ShipSystem {
    
	public EngineerController ec;
	Rigidbody rb;

	const int generatorHealth = 3;
    protected override void Start() {
        base.Start();
        health = generatorHealth;
		rb = GetComponent<Rigidbody> ();
    }

    protected override void ResetHealth() {
        health = generatorHealth;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!broken) {
            rb.useGravity = true;
            enabled = false;
			ec.ResumeGravity ();
        }
    }

	protected override void Break ()
	{
		base.Break ();
		transform.position += new Vector3 (0f, 1f, 0f);
		rb.useGravity = false;
		ec.LoseGravity ();
	}
}
