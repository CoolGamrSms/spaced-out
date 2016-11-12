using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonSequence : MonoBehaviour {
    string[] buttons = new string[4];
    GameObject[] buttonImages = new GameObject[4];
    Toggle[] toggles;

    int numCorrect = 0;
    public int correctRequired = 3;

    int pressedButton = -1;
    int correctButton = 0;
    int joystickNum;

    float timer = 3f;
    public float timeLimit = 3f;
	// Use this for initialization
	
    void Start () {
        joystickNum = transform.parent.GetComponent<ObjectBase>().joystickNum;
	 
        for (int i = 0; i < 4; ++i)
        {
            buttonImages[i] = transform.GetChild(i).gameObject;
            buttons[i] = "joystick " + joystickNum + " button " + i;
        }
        toggles = transform.GetComponentsInChildren<Toggle>();
        this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(buttons[0]))
        {
            pressedButton = 0;
        }
        else if (Input.GetKeyDown(buttons[1])){
            pressedButton = 1;
        }else if (Input.GetKeyDown(buttons[2]))
        {
            pressedButton = 2;
        }else if (Input.GetKeyDown(buttons[3]))
        {
            pressedButton = 3;
        }


        if (pressedButton == correctButton)
        {
            toggles[numCorrect].isOn = true;
            SetNextButton();
            ++numCorrect;
        }else if (timer > timeLimit || (pressedButton != -1)) {
            Reset();
        }

        if (numCorrect == correctRequired) {
            
            buttonImages[correctButton].SetActive(false);
            GetComponentInParent<ObjectBase>().broken = false;
            this.enabled = false;
        }
        timer += Time.deltaTime;
        pressedButton = -1;
	}

    void SetNextButton()
    {
        buttonImages[correctButton].SetActive(false);
        correctButton = Random.Range(0, 100) % 4;
        buttonImages[correctButton].SetActive(true);
    }

    void Reset() { 
        foreach (Toggle tog in toggles)
        {
            tog.isOn = false;
        }
        SetNextButton();
        numCorrect = 0;
        timer = 0f;

    }
}
