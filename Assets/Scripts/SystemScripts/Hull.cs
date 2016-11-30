using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
	SpriteRenderer breachSprite;
    ParticleSystem particles;
	const int hullHealth = 3;
    bool fixonce;

    public Image hullImg;

    static int numHull = 0;

    protected override void Start() {
        breachSprite = GetComponent<SpriteRenderer>();
        particles = GetComponentInChildren<ParticleSystem>();
        base.Start();
        health = hullHealth;
		breachSprite.enabled = false;
        particles.Stop();
        fixonce = true;
        hullImg.enabled = false;
    }

    // Enabled when Engineer in range
    void FixedUpdate() {
        if (!broken && !fixonce) {
			breachSprite.enabled = false;
            particles.Stop();
            sc.FixBreach ();
            fixonce = true;
        }
    }

	protected override void Break () {
		base.Break ();
        fixonce = false;
		breachSprite.enabled = true;
        particles.Play();
		sc.HullBreach ();
        hullImg.enabled = true;
        numHull++;
	}

    protected override void ResetHealth() {
        health = hullHealth;
        numHull--;
        if (numHull == 0) {
            hullImg.enabled = false;
        }
    }
}
