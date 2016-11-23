using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
	SpriteRenderer breachSprite;

	const int hullHealth = 2;
	public ShipController sc;

    protected override void Start() {
        breachSprite = GetComponent<SpriteRenderer>();
        base.Start();
        health = hullHealth;
		breachSprite.enabled = false;
		//sc = transform.parent.GetComponent<ShipController> ();
    }

    protected override void ResetHealth() {
        health = hullHealth;
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (!broken) {
			breachSprite.enabled = false;
			sc.FixBreach ();
        }
    }

	protected override void Break ()
	{
		base.Break ();
        Debug.Log("Hull breached");
		breachSprite.enabled = true;
		sc.HullBreach ();
	}
}
