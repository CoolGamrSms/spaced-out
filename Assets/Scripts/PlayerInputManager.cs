using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour {


    public int numPlayers {
        get {
            return controllers.Count;
        }
    }
    public List<InputDevice> controllers { get; private set; }


    // Update is called once per frame
    void FixedUpdate() {
        if (InputManager.ActiveDevice.CommandWasPressed) {
            if (numPlayers < 0) return;
            SceneManager.LoadScene("RaceScene");
        }
        if (InputManager.ActiveDevice.Action1.WasPressed) {
            foreach (InputDevice controller in controllers) {
                if (controller == InputManager.ActiveDevice)
                    return;
            }
            controllers.Add(InputManager.ActiveDevice);
        }
    }

    public static PlayerInputManager Instance = null;
    void Awake() {
        if (Instance != null && Instance != this) {
            Debug.LogError("Destroying Object: Instance already exists.");
            Destroy(gameObject);
            return;
        }
        else
            Instance = this;
        controllers = new List<InputDevice>();
    }
}
