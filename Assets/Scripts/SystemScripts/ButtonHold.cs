using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class ButtonHold : Repair {

    public float timeLimit = 3f;

    private Slider timer;
    private Canvas canvas;
    ShipSystem system;
    bool fixing;

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
        canvas.enabled = system.interacting && system.broken;
        if (system.interacting && eController.Action3.WasPressed) fixing = true;

        if (fixing)
        {
            if (eController.Action3.WasReleased) fixing = false;
            if (!system.interacting) fixing = false;
        }

        if (fixing && system.interacting) {
            timer.value += Time.deltaTime;
            if (timer.value >= timeLimit && system.broken) {
                timer.value = 0;
                system.Fixed();
            }
        }
        else {
            timer.value = 0f;
        }
    }

    public override void SetBroken() {
        timer.value = 0f;
        fixing = false;
    }
}
