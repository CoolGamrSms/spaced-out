using UnityEngine;
using System.Collections;

public class HullBreach : ObjectBase {

    //Controlled by ship script
    public bool broken = false;
    public float timeLimit = 50f;

    private string inputAxis;
    private float timer = 0;
	// Use this for initialization
	void Start () {
        // inputAxis = "joystick " + GetComponent<ObjectBase>().JoystickNum + " button 2";
        this.enabled = false;
        inputAxis = "joystick 1 button 2";
	}
	
	// Only enabled when Engineer in range
	void FixedUpdate () {
        if (Input.GetKey(inputAxis)) {
            timer += Time.deltaTime;
            if (timer > timeLimit) {
                //fixed, remove negative effect on ship
                print("fixed");
                timer = 0;
                gameObject.SetActive(false);
            }

        }
	}

    void OnTriggerEnter( Collider col) {
        if (col.gameObject.tag == "Engineer" && broken) {
            this.enabled = true;
        }
    }

    void OnTriggerExit( Collider col) {
        if (col.gameObject.tag == "Engineer" && broken){
            this.enabled = false;
            timer = 0;
        }
    }
}
