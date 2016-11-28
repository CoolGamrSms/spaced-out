using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour {

	void Update() {
		if (Input.GetButtonDown ("Fire1")) {
			SceneManager.LoadScene("RaceScene");
		}
	}
}
