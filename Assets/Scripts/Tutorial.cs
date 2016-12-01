using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using InControl;

public class Tutorial : MonoBehaviour {

	public Sprite intro2;
	public Sprite intro3;

	int x = 0;

	void Update() {
		if (InputManager.ActiveDevice.Action1.WasPressed) {
			ChangeIntro ();
		}
	}

	public void ChangeIntro() {
		if (x == 0) {
			GetComponentInChildren<Image>().sprite = intro2;
		}
		else if (x == 1) {
			GetComponentInChildren<Image>().sprite = intro3;
		}
		else if (x == 2) {
			SceneManager.LoadScene("RaceScene");
		}

		x++;
	}
}
