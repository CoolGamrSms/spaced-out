using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHold : Repair {

    public float timeLimit = 3f;

    private string inputAxis;
    private Slider timer;
	private Canvas canvas;
    ShipSystem system;
    // Use this for initialization
    void Start() {
        system = GetComponentInParent<ShipSystem>();
        inputAxis = "joystick " + system.joystickNum + " button 2";
        timer = GetComponentInChildren<Slider>();
        timer.maxValue = timeLimit;
		canvas = GetComponent<Canvas> ();
		canvas.enabled = false;
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (Input.GetKey(inputAxis)) {
            timer.value += Time.deltaTime;
            if (timer.value >= timeLimit) {
                timer.value = 0;
                system.Fixed();
                enabled = false;
            }

        }
        else {
            timer.value = 0f;
        }
    }

	public override void SetBroken ()
	{
		canvas.enabled = true;
	}
		
}
