using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class Start : MonoBehaviour {
    //int buttonDex = 0;
    //Button[] buttons;

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        //buttons = GetComponentsInChildren<Button>();
    }

    void FixedUpdate() {
        /*buttons[buttonDex].Select();
        if(InputManager.ActiveDevice.DPadDown.WasPressed || 
            InputManager.ActiveDevice.LeftStickDown.WasPressed) {
            if (buttonDex == 2)
                buttonDex = 0;
            else
                ++buttonDex;
        }
        else if (InputManager.ActiveDevice.DPadUp.WasPressed ||
                    InputManager.ActiveDevice.LeftStickUp.WasPressed) {
            if (buttonDex == 0)
                buttonDex = 2;
            else
                --buttonDex;
        }
        else if (InputManager.ActiveDevice.Action1.WasPressed) {
            buttons[buttonDex].onClick.Invoke();
        }*/
    }

	public void StartGame() {
		SceneManager.LoadScene("ReadyCheck");
	}

    public void MainMenu() {
        SceneManager.LoadScene("start");
    }

	public void Instructions() {
		SceneManager.LoadScene("Instructions");
	}

	public void Quit() {
		Application.Quit();
	}
}
