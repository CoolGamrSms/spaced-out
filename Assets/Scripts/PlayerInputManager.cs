using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour {
    bool gameStarted = false;

    public int numPlayers {
        get {
            // return controllers.Count;
            return controllers.Length;
        }
    }
    //public List<InputDevice> controllers { get; private set; }

    public InputDevice[] controllers = new InputDevice[4];
    //0 - pilot1, 1 - engineer1, 2-pilot2, 3-engineer2

    // Update is called once per frame
    void FixedUpdate() {
        if (ActiveDevice.CommandWasPressed && !gameStarted) {
            gameStarted = true;
            SceneManager.LoadScene("RaceScene");
        }
        if (ActiveDevice.Action1.WasPressed && !gameStarted) {
            foreach (InputDevice controller in controllers) {
                if (controller == ActiveDevice)
                    return;
            }
            controllers[0] = ActiveDevice;
        }

        if (ActiveDevice.Action2.WasPressed && !gameStarted)
        {
            foreach (InputDevice controller in controllers)
            {
                if (controller == ActiveDevice)
                    return;
            }
            controllers[1] = ActiveDevice;

        }

        if (ActiveDevice.Action3.WasPressed && !gameStarted)
        {
            foreach (InputDevice controller in controllers)
            {
                if (controller == ActiveDevice)
                    return;
            }
            controllers[2] = ActiveDevice;

        }

        if (ActiveDevice.Action4.WasPressed && !gameStarted)
        {
            foreach (InputDevice controller in controllers)
            {
                if (controller == ActiveDevice)
                    return;
            }
            controllers[3] = ActiveDevice;

        }

        if (ActiveDevice.RightBumper.IsPressed && ActiveDevice.LeftBumper.IsPressed) {
#if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
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
        //controllers = new List<InputDevice>();
        controllers = new InputDevice[4];
    }

    InputDevice ActiveDevice {
        get {
            return InputManager.ActiveDevice;
        }
    }
}
