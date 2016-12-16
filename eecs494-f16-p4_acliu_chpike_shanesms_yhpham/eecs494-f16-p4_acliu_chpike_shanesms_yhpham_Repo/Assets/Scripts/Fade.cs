using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade : MonoBehaviour {
	CanvasGroup panel;
	static GameObject fadingCanvas = null;

	void Awake() {
		if (fadingCanvas == null) {
			DontDestroyOnLoad (gameObject);
			fadingCanvas = gameObject;
		}
		panel = GetComponentInChildren<CanvasGroup> ();
	}

	void OnEnable() {
		SceneManager.sceneUnloaded += FadeScreen;
	}

	void OnDisable() {
		SceneManager.sceneUnloaded -= FadeScreen;
	}

	void FadeScreen(Scene scene) {
		panel.alpha = 1f;
		StartCoroutine ("FadeIn");	
	}

	IEnumerator FadeOut() {
		while (panel.alpha < 1f) {
			panel.alpha += .2f;
			yield return new WaitForSeconds(.1f);

		}

		StartCoroutine ("FadeIn");
	}

	IEnumerator FadeIn() {
		while (panel.alpha > 0f) {
			panel.alpha -= .1f;
			yield return new WaitForSeconds (.1f);
		}


	}
}
