using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class End : MonoBehaviour {

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ship1") {
			SceneManager.LoadScene("Gameover1");
		}
		else if (col.gameObject.tag == "Ship2") {
			SceneManager.LoadScene("Gameover2");
		}
	}
}
