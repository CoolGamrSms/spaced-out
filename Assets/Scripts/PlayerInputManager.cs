using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour {
    bool gameStarted = false;

    public int numPlayers {
        get {
            return controllers.Length;
        }
    }

    const string join = "Press A to join";
    const string select = "Select team/role (L Stick)";
    const string starts = "Press Start";

    public InputDevice[] controllers;
    int joined;
    public Text bottomText;
    public ControllerInterface[] controllerInterfaces;
    //0 - pilot1, 1 - engineer1, 2-pilot2, 3-engineer2

    int GetSlot(ControllerInterface.ContState cont) {
        bool slot0 = false;
        bool slot1 = false;
        foreach (ControllerInterface i in controllerInterfaces) {
            if (i.cont == cont && i.mySlot == 0) slot0 = true;
            else if (i.cont == cont && i.mySlot == 1) slot1 = true;
        }
        if (slot0 && slot1) return -1;
        if (!slot0) return 0;
        return 1;
    }

    bool CheckRole(ControllerInterface.ContState cont, ControllerInterface.RoleState role) {
        foreach (ControllerInterface i in controllerInterfaces) {
            if (i.role == role && i.cont == cont) return true;
        }
        return false;
    }

    bool CheckDevice(InputDevice id) {
        foreach(ControllerInterface i in controllerInterfaces) {
            if (i.ind == id) return true;
        }
        return false;
    }

    bool CheckAllReady() {
        foreach (ControllerInterface i in controllerInterfaces) {
            if (!i.isActiveAndEnabled) return false;
            if (i.role == ControllerInterface.RoleState.NONE) return false;
        }
        return true;
    }

    void FormatControllers() {
        //Format the controllers array properly
        foreach (ControllerInterface i in controllerInterfaces) {
            if (i.role == ControllerInterface.RoleState.PILOT && i.cont == ControllerInterface.ContState.BLUE) controllers[0] = i.ind;
            if (i.role == ControllerInterface.RoleState.ENG && i.cont == ControllerInterface.ContState.BLUE) controllers[1] = i.ind;
            if (i.role == ControllerInterface.RoleState.PILOT && i.cont == ControllerInterface.ContState.RED) controllers[2] = i.ind;
            if (i.role == ControllerInterface.RoleState.ENG && i.cont == ControllerInterface.ContState.RED) controllers[3] = i.ind;
        }
    }

    void SetBottomString() {
        if (CheckAllReady()) bottomText.text = starts;
        else if (joined == 4) bottomText.text = select;
        else bottomText.text = join;
    }

    void FixedUpdate() {
        if(!gameStarted) {
            //Process start press
            if(ActiveDevice.CommandWasPressed && CheckAllReady()) {
                FormatControllers();
                gameStarted = true;
                SceneManager.LoadScene("Tutorial");
            }
            //Go back to main menu
            if(ActiveDevice.Action2.WasPressed) {
                Destroy(gameObject);
                SceneManager.LoadScene("start");
            }
            //New controller registering
            if(ActiveDevice.Action1.WasPressed && !CheckDevice(ActiveDevice)) {
                controllerInterfaces[joined].ind = ActiveDevice;
                controllerInterfaces[joined++].gameObject.SetActive(true);
                SetBottomString();
            }
            //Handle input from all controllers
            foreach (ControllerInterface i in controllerInterfaces) {
                if (!i.isActiveAndEnabled) continue;
                if (!i.ind.LeftStickX.WasPressed && !i.ind.LeftStickY.WasPressed) continue;
                //Horizontal Input + no role
                if (Mathf.Abs(i.ind.LeftStickX.Value) > Mathf.Abs(i.ind.LeftStickY.Value) && i.role == ControllerInterface.RoleState.NONE) {
                    //Move from main to blue
                    if (i.cont == ControllerInterface.ContState.MAIN && i.ind.LeftStickX.Value < 0f) {
                        int slot = GetSlot(ControllerInterface.ContState.BLUE);
                        if (slot < 0) continue;
                        i.cont = ControllerInterface.ContState.BLUE;
                        i.MoveTeam(-200f, slot);
                    }
                    //Move from main to red
                    else if (i.cont == ControllerInterface.ContState.MAIN && i.ind.LeftStickX.Value > 0f) {
                        int slot = GetSlot(ControllerInterface.ContState.RED);
                        if (slot < 0) continue;
                        i.cont = ControllerInterface.ContState.RED;
                        i.MoveTeam(200f, slot);
                    }
                    //Move back to main
                    else if (i.cont == ControllerInterface.ContState.BLUE && i.ind.LeftStickX.Value > 0f) {
                        i.Home();
                    }
                    else if (i.cont == ControllerInterface.ContState.RED && i.ind.LeftStickX.Value < 0f) {
                        i.Home();
                    }
                }
                //Vertical Input + has team 
                else if (Mathf.Abs(i.ind.LeftStickY.Value) > Mathf.Abs(i.ind.LeftStickX.Value) && i.cont != ControllerInterface.ContState.MAIN) {
                    //Move to engineer
                    if(i.ind.LeftStickY.Value < 0 && i.role == ControllerInterface.RoleState.NONE) {
                        if (CheckRole(i.cont, ControllerInterface.RoleState.ENG)) continue;
                        i.role = ControllerInterface.RoleState.ENG;
                        i.Move(-80f);
                        SetBottomString();
                    }
                    //Move to pilot
                    else if (i.ind.LeftStickY.Value > 0 && i.role == ControllerInterface.RoleState.NONE) {
                        if (CheckRole(i.cont, ControllerInterface.RoleState.PILOT)) continue;
                        i.role = ControllerInterface.RoleState.PILOT;
                        i.Move(80f);
                        SetBottomString();
                    }
                    //Move to team home
                    else if (i.ind.LeftStickY.Value < 0 && i.role == ControllerInterface.RoleState.PILOT) {
                        i.TeamHome();
                        SetBottomString();
                    }
                    else if (i.ind.LeftStickY.Value > 0 && i.role == ControllerInterface.RoleState.ENG) {
                        i.TeamHome();
                        SetBottomString();
                    }
                }
            }
        }

		//forfeits if pilot and engineer both hold the back button
		//1 - Blue Team
		if(controllers[0].CommandIsPressed &&  controllers[1].CommandIsPressed){
			SceneManager.LoadScene ("Gameover2");
		}
		//2-Red Team
		if (controllers [2].CommandIsPressed && controllers [3].CommandIsPressed) {
			SceneManager.LoadScene ("Gameover1");
		}
    }

    public static PlayerInputManager Instance = null;

    void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogError("Destroying Object: Instance already exists.");
            Destroy(gameObject);
            return;
        }
        else {
            Instance = this;
        }

        controllers = new InputDevice[4];
        joined = 0;
    }

    InputDevice ActiveDevice {
        get {
            return InputManager.ActiveDevice;
        }
    }
}
