using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Engine : ShipSystem {

    const int engineHealth = 5;

    public Material m2;
    Material m;

    public GameObject title;

    protected override void Start() {
        base.Start();
        health = engineHealth;
        m = GetComponent<Renderer>().material;
    }

	protected override void Break () {
		base.Break ();
		sc.BreakEngine ();
        GetComponent<Renderer>().material = m2;
        title.GetComponent<TextMesh>().color = Color.red;
	}

    protected override void ResetHealth() {
		sc.FixEngine ();
        health = engineHealth;
        GetComponent<Renderer>().material = m;
        title.GetComponent<TextMesh>().color = Color.green;
    }
}
