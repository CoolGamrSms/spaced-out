using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StopMusic : MonoBehaviour {

	public AudioSource bgMusic;

	void Update () {
		if (SceneManager.GetActiveScene().name == "RaceScene") {
			bgMusic.Stop();
		}
	}
}
