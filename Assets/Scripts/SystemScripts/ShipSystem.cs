
using UnityEngine;
using System.Collections;

public class ShipSystem : MonoBehaviour {

    //set by ship script
    public int joystickNum;
    public bool broken { get; private set; }
    Repair repairScript;

    // Use this for initialization
    virtual protected void Start() {
        repairScript = GetComponentInChildren<Repair>();
        repairScript.enabled = false;
        enabled = false;
        broken = false;
    }

    virtual protected void OnTriggerEnter(Collider col) {
        if (col.gameObject.CompareTag("Engineer")) {
            if (broken) {
                repairScript.enabled = true;
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

	virtual protected void Break(){
		broken = true;
	}
		
    public int health { get; protected set; }

    public void TakeDamage(int damage) {
        if (health == 0)
            return;

        if (health - damage < 0)
            damage = 0;

        health -= damage;
        if (health <= 0) {
			Break();
			repairScript.SetBroken ();
        }
    }

    protected virtual void ResetHealth() { }
}
