using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class Start : MonoBehaviour {

	public void StartGame() {
		SceneManager.LoadScene("ReadyCheck");
	}

    public void MainMenu() {
        SceneManager.LoadScene("Start");
    }

	public void Quit() {
		Application.Quit();
	}
}
