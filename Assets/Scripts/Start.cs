using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Start : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene("scene_0");
	}

	public void Instructions() {
		SceneManager.LoadScene("Instructions");
	}

	public void Quit() {
		Application.Quit();
	}
}
