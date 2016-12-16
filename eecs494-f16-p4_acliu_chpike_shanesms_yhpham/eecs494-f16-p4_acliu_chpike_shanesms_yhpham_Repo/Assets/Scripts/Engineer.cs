using UnityEngine;
using System.Collections;
using InControl;

public class Engineer : MonoBehaviour {
    public int playerNumber;
    protected InputDevice eController;

	void Awake () {
        eController = PlayerInputManager.Instance.controllers[playerNumber];
	}
}
