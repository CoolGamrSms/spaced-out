using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityGenerator : ShipSystem {

	const int generatorHealth = 5;
    
	public EngineerController ec;
	Rigidbody rb;

	public GameObject title;

    protected override void Start() {
        rb = GetComponent<Rigidbody>();
        base.Start();
        health = generatorHealth;
    }

	protected override void Break () {
		base.Break ();
		transform.position += new Vector3 (0f, 1f, 0f);
		rb.useGravity = false;
		ec.LoseGravity ();
		title.GetComponent<TextMesh>().color = Color.red;
	}

    protected override void ResetHealth() {
		rb.useGravity = true;
		ec.ResumeGravity ();
        health = generatorHealth;
        title.GetComponent<TextMesh>().color = Color.green;
    }
}
