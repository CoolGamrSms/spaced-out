using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using InControl;

public class Tutorial : MonoBehaviour {

	public Sprite intro1;
	public Sprite intro2;
	public Sprite intro3;

	int x = 0;
	float waitTime = 2;

	void Update() {
		waitTime -= Time.deltaTime;

		if (waitTime <= 0.0f) {
			if (InputManager.ActiveDevice.Action1.WasPressed) {
				ChangeIntroForward();
			}
			else if (InputManager.ActiveDevice.Action2.WasPressed) {
				ChangeIntroBack();
			}
		}
	}

	public void ChangeIntroForward() {
		waitTime = 2;
		x++;

		if (x == 1) {
			GetComponentInChildren<Image>().sprite = intro2;
		}
		else if (x == 2) {
			GetComponentInChildren<Image>().sprite = intro3;
		}
		else if (x == 3) {
			SceneManager.LoadScene("RaceScene");
		}
	}

	public void ChangeIntroBack() {
		waitTime = 2;
		x--;

		if (x == -1) {
			SceneManager.LoadScene("ReadyCheck");
		}
		else if (x == 0) {
			GetComponentInChildren<Image>().sprite = intro1;
		}
		else if (x == 1) {
			GetComponentInChildren<Image>().sprite = intro2;
		}
		else if (x == 2) {
			GetComponentInChildren<Image>().sprite = intro3;
		}
	}
}
