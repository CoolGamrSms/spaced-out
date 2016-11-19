
using UnityEngine;
using System.Collections;

public class ShipSystem : MonoBehaviour {

    //set by ship script
    public int joystickNum;
    public bool broken = false;
    Repair repairScript;

    // Use this for initialization
    virtual protected void Start() {
        repairScript = GetComponentInChildren<Repair>();
        repairScript.enabled = false;
        enabled = false;
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

        health -= damage;
        if (health <= 0) {
			Break();
			repairScript.SetBroken ();
        }
    }
    protected virtual void ResetHealth() { }

}
