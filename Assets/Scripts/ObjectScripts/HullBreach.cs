using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HullBreach : ObjectBase {

    public float timeLimit = 50f;

    private string inputAxis;
    private Slider timer;
	// Use this for initialization
	void Start () {
        // inputAxis = "joystick " + GetComponent<ObjectBase>().JoystickNum + " button 2";
        this.enabled = false;
        inputAxis = "joystick 1 button 2";
        timer = GetComponentInChildren<Slider>();
        timer.maxValue = timeLimit;
	}
	
	// Only enabled when Engineer in range
	void FixedUpdate () {
        if (Input.GetKey(inputAxis)) {
            timer.value += Time.deltaTime;
            if (timer.value >= timeLimit) {
                //fixed, remove negative effect on ship
                print("fixed");
                timer.value = 0;
                broken = false;
                gameObject.SetActive(false);
            }

        }
	}

    override protected void OnTriggerExit( Collider col) {
        base.OnTriggerExit(col);
        timer.value = 0;
    }
}
