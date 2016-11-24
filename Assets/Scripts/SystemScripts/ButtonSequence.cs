using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class ButtonSequence : Repair {
    Toggle[] toggles;
    GameObject[] buttonImages = new GameObject[4];

    int numCorrect = 0;
    public int correctRequired = 3;

    int pressedButton = -1;
    int correctButton = 0; 

    float timer = 3f;
    public float timeLimit = 3f;

    ShipSystem system;
    // Use this for initialization

    public void Start() {
        system = GetComponentInParent<ShipSystem>();

        for (int i = 0; i < 4; ++i) {
            buttonImages[i] = transform.GetChild(i).gameObject;
        }
        toggles = transform.GetComponentsInChildren<Toggle>();
        print(transform.parent.name + "'s num toggles: " + toggles.Length);
    }

    // Update is called once per frame
    void FixedUpdate() {
   
        if (eController.Action1.WasPressed) {
            pressedButton = 0;
        }
        else if (eController.Action2.WasPressed) {
            pressedButton = 1;
        }
        else if (eController.Action3.WasPressed) {
            pressedButton = 2;
        }
        else if (eController.Action4.WasPressed) {
            pressedButton = 3;
        }

        if (pressedButton == correctButton) {
            toggles[numCorrect].isOn = true;
            SetNextButton();
            ++numCorrect;
        }
        else if (timer > timeLimit || (pressedButton != -1)) {
            Reset();
        }

        if (numCorrect == correctRequired) {
            buttonImages[correctButton].SetActive(false);
            system.Fixed();
            enabled = false;
        }
        timer += Time.deltaTime;
        pressedButton = -1;
    }

    void SetNextButton() {
        buttonImages[correctButton].SetActive(false);
        correctButton = Random.Range(0, 100) % 4;
        buttonImages[correctButton].SetActive(true);
    }

	override public void SetBroken ()
	{
		TurnTogglesOff ();
        Debug.Log("breaking: " + transform.parent.name);
	}

    void Reset() {
		TurnTogglesOff ();
        SetNextButton();
        numCorrect = 0;
        timer = 0f;
    }

	void TurnTogglesOff(){
		foreach (Toggle tog in toggles) {
			tog.isOn = false;
		}
	}
}
