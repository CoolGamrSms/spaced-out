using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StopMusic : MonoBehaviour {

	public AudioSource bgMusic;
	public bool playing = true;

	void Update () {
		if (SceneManager.GetActiveScene().name == "RaceScene") {
			playing = false;
			bgMusic.Stop();
		}
		else if (SceneManager.GetActiveScene().name == "Gameover1" 
			|| SceneManager.GetActiveScene().name == "Gameover2") {
			if (playing == false) {
				playing = true;
				bgMusic.Play();
			}
		}
	}
}
