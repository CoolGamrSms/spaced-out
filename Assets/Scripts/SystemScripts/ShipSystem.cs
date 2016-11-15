
using UnityEngine;
using System.Collections;

public class ShipSystem : MonoBehaviour {

    //set by ship script
    public int joystickNum;
    public bool broken = false;
    public Repair fixScript;

    // Use this for initialization
    virtual protected void Start() {
        fixScript = GetComponentInChildren<Repair>();
        fixScript.enabled = false;
        enabled = false;
    }

    virtual protected void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Engineer")) {
            if (broken) {
                fixScript.enabled = true;
            }
            else {
                enabled = true;
            }
        }
    }

    virtual protected void OnTriggerExit(Collider col) {
        if (col.gameObject.CompareTag("Engineer")) {
            enabled = false;
        }
    }

    public void Fixed() {
        broken = false;
        enabled = true;
        ResetHealth();
    }

    public int health { get; protected set; }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            broken = true;
            enabled = false;
        }
    }
    protected virtual void ResetHealth() { }
}
