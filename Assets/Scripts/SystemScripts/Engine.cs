using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Engine : ShipSystem {

    [Range(0, 100)]
    public int engineHealth = 5;

    public Material m2;
    Material m;

    public GameObject title;
    public Image engineImg;

    protected override void Start() {
        base.Start();
        health = engineHealth;
        m = GetComponent<Renderer>().material;
        engineImg.enabled = false;
    }

	protected override void Break () {
		base.Break ();
		sc.BreakEngine ();
        GetComponent<Renderer>().material = m2;
        title.GetComponent<TextMesh>().color = Color.red;
        engineImg.enabled = true;
	}

    protected override void ResetHealth() {
		sc.FixEngine ();
        health = engineHealth;
        GetComponent<Renderer>().material = m;
        title.GetComponent<TextMesh>().color = Color.green;
        engineImg.enabled = false;
    }
}
