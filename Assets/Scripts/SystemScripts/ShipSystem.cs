
using UnityEngine;
using System.Collections;
using System;

public class ShipSystem : Engineer {

    Repair repairScript;
    [HideInInspector]
    public ShipController sc;
    public bool broken { get; private set; }
    public bool interacting { get; private set; }

    // Use this for initialization
    virtual protected void Start() {
        repairScript = GetComponentInChildren<Repair>();
        broken = false;
        interacting = false;
    }

    public void StartInteraction() {
        Debug.Log("Interacting: "+name);
        interacting = true;
    }
    public void EndInteraction() {
        Debug.Log("End Interaction: " + name);
        interacting = false;
    }

    public void Fixed() {
        broken = false;
        ResetHealth();
    }

    virtual protected void Break() {
        broken = true;
        repairScript.SetBroken();
    }

    public int health { get; protected set; }

    public void TakeDamage(int damage) {
        Debug.Log(name + " || Damage: " + damage.ToString() + " Health: " + health.ToString());
        if (health == 0)
            return;

        health -= damage;
        if (health <= 0) {
            health = 0;
            Break();
        }
    }

    protected virtual void ResetHealth() { }

}
