using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHold : Repair {

    public float timeLimit = 3f;

    private string inputAxis;
    private Slider timer;
    ShipSystem ob;
    // Use this for initialization
    void Start() {
        ob = GetComponentInParent<ShipSystem>();
        inputAxis = "joystick " + ob.joystickNum + " button 2";
        timer = GetComponentInChildren<Slider>();
        timer.maxValue = timeLimit;
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (Input.GetKey(inputAxis)) {
            timer.value += Time.deltaTime;
            if (timer.value >= timeLimit) {
                timer.value = 0;
                ob.Fixed();
                enabled = false;
            }

        }
        else {
            timer.value = 0f;
        }
    }
}
