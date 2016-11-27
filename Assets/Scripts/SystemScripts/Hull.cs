using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
	SpriteRenderer breachSprite;
    ParticleSystem particles;
	const int hullHealth = 2;
    bool fixonce;

    protected override void Start() {
        breachSprite = GetComponent<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        base.Start();
        health = hullHealth;
		breachSprite.enabled = false;
        particles.Stop();
        fixonce = true;
    }

    protected override void ResetHealth() {
        health = hullHealth;
        Debug.LogError(health);
        Debug.LogError(broken);
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (!broken && !fixonce) {
			breachSprite.enabled = false;
            particles.Stop();
            sc.FixBreach ();
            fixonce = true;
        }
    }

	protected override void Break ()
	{
		base.Break ();
        fixonce = false;
        Debug.Log("Hull breached");
		breachSprite.enabled = true;
        particles.Play();
		sc.HullBreach ();
	}
}
