using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class ButtonSequence : Repair {
    Toggle[] toggles;
    GameObject[] buttonImages = new GameObject[4];

    int numCorrect = 3;
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
        TurnTogglesOn();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!system.broken) return;
        if (!system.interacting) {
            pressedButton = -1;
            buttonImages[correctButton].SetActive(false);
        }
        else if (eController.Action1.WasPressed) {
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
        if(system.interacting)
        {
            buttonImages[correctButton].SetActive(true);
        }

        if (pressedButton == correctButton) {
            toggles[numCorrect].isOn = true;
            SetNextButton();
            timer = 0f;
            ++numCorrect;
        }
        else if (timer > timeLimit || (pressedButton != -1))
        {
            Reset();
        }

        if (numCorrect >= correctRequired)
        {
            buttonImages[correctButton].SetActive(false);
            system.Fixed();
            TurnTogglesOn();
        }


        if(numCorrect > 0) timer += Time.deltaTime;
        pressedButton = -1;
    }

    void SetNextButton() {
        buttonImages[correctButton].SetActive(false);
        int prev = correctButton;
        while(correctButton == prev) correctButton = Random.Range(0, 100) % 4;
        if(system.interacting) buttonImages[correctButton].SetActive(true);
    }

	override public void SetBroken ()
	{
		Reset ();
        Debug.LogWarning("breaking: " + transform.parent.name);
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
    void TurnTogglesOn()
    {
        foreach (Toggle tog in toggles)
        {
            tog.isOn = true;
        }
    }
}
