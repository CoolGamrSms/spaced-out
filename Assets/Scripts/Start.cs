using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class Start : MonoBehaviour {

	public void Awake() {
		Cursor.visible = false;
	}

	public void StartGame() {
		PlayerInputManager.Instance.Clear ();
		SceneManager.LoadScene("ReadyCheck");
	}

    public void MainMenu() {
		PlayerInputManager.Instance.Clear ();
        SceneManager.LoadScene("Start");
    }

	public void Quit() {
		Application.Quit();
	}
}
