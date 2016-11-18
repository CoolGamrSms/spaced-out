using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
	SpriteRenderer breachSprite;

	const int hullHealth = 1;
	ShipController sc;

    protected override void Start() {
        base.Start();
        health = hullHealth;
		breachSprite = GetComponent<SpriteRenderer> ();
		breachSprite.enabled = false;
		sc = transform.parent.GetComponent<ShipController> ();
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
		breachSprite.enabled = true;
		sc.HullBreach ();
	}
}
