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
        if (PlayerInputManager.Instance != null) 
        	Destroy(PlayerInputManager.Instance.gameObject);
		SceneManager.LoadScene("ReadyCheck");
	}

    public void MainMenu() {
        if (PlayerInputManager.Instance != null) 
        	Destroy(PlayerInputManager.Instance.gameObject);
        SceneManager.LoadScene("Start");
    }

	public void Quit() {
        //Removed for showcase purposes
		//Application.Quit();
	}
}
