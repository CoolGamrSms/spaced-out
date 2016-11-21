using UnityEngine;
using System.Collections;
using InControl;

public class Engineer : MonoBehaviour {
    public int playerNumber;
    protected InputDevice eController;

	// Use this for initialization
	void Awake () {
        eController = PlayerInputManager.Instance.controllers[playerNumber];
	}
}
