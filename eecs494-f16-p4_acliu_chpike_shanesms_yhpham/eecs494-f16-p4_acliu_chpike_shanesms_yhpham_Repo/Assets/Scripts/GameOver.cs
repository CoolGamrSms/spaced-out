using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
	public CanvasGroup ui;
	bool visible = false;

	void Update() {
		if (visible) {
			ui.alpha += .05f;
			if (ui.alpha == 1f) {
				Destroy (this);
			}
		}
	}

	public void Gameover() {
		ui.gameObject.SetActive (true);
		visible = true;
	}
}
