using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CommandCenter : ShipSystem {

    const int commandHealth = 5;

	public Material brokenMat;
	Material normalMat;

	MeshRenderer mr;

    public GameObject title;
    public Image cmdImg;

    protected override void Start() {
        base.Start();
        health = commandHealth;

		mr = GetComponent<MeshRenderer> ();
		normalMat = mr.material;
        cmdImg.enabled = false;
    }

    protected override void Break() {
        base.Break();
        sc.BreakCommandCenter();
		mr.material = brokenMat;
        title.GetComponent<TextMesh>().color = Color.red;
        cmdImg.enabled = true;
    }

    protected override void ResetHealth() {
        health = commandHealth;
        sc.FixedCommandCeneter();
		mr.material = normalMat;
        title.GetComponent<TextMesh>().color = Color.green;
        cmdImg.enabled = false;
    }
}
