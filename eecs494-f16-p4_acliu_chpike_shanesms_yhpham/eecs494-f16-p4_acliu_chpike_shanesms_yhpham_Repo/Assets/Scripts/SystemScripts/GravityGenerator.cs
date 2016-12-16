using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GravityGenerator : ShipSystem {
    [Range(0, 10)]
    public int generatorHealth = 10;
    
	public EngineerController ec;
	Rigidbody rb;

	public GameObject title;
	public Image gravityImg;

    protected override void Start() {
        rb = GetComponent<Rigidbody>();
        base.Start();
        health = generatorHealth;
        gravityImg.enabled = false;
    }

	protected override void Break () {
		base.Break ();
		transform.position += new Vector3 (0f, 1f, 0f);
		rb.useGravity = false;
		ec.LoseGravity ();
		title.GetComponent<TextMesh>().color = Color.red;
		gravityImg.enabled = true;
	}

    protected override void ResetHealth() {
		rb.useGravity = true;
		ec.ResumeGravity ();
        health = generatorHealth;
        title.GetComponent<TextMesh>().color = Color.green;
        gravityImg.enabled = false;
    }
}
