using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class ButtonHold : Repair {

    public float timeLimit = 3f;

    private Slider timer;
    private Canvas canvas;
    ShipSystem system;

    // Use this for initialization
    void Start() {
        system = GetComponentInParent<ShipSystem>();
        timer = GetComponentInChildren<Slider>();
        timer.maxValue = timeLimit;
        canvas = GetComponentInChildren<Canvas>();
        canvas.enabled = false;
    }

    // Only enabled when Engineer in range
    void FixedUpdate() {
        if (eController.Action2.IsPressed) {
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

    public override void SetBroken() {
        canvas.enabled = true;
    }
}
