using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Hull : ShipSystem {
    [Range(0, 10)]
    public int hullHealth = 5;

    SpriteRenderer breachSprite;
    ParticleSystem particles;
    
    bool fixonce;

    public Image hullImg;

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
			if (sc.numHullBreaches == 0) {
				hullImg.enabled = false;
			}
        }
    }

    public void BreakSelf() {
        Break();
    }

	protected override void Break () {
		base.Break ();
        fixonce = false;
		breachSprite.enabled = true;
        particles.Play();
		sc.HullBreach ();
        hullImg.enabled = true;
	}

    protected override void ResetHealth() {
        health = hullHealth;
        
    }
}
