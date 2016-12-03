using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class End : MonoBehaviour {

    public Slider slider1;
    public Slider slider2;
    public GameObject ship1;
    public GameObject ship2;
    float dist1, dist2;

    void Start() {
        dist1 = Vector3.Distance(transform.position, ship1.transform.position);
        dist2 = Vector3.Distance(transform.position, ship2.transform.position);
    }

    void FixedUpdate() {
        slider1.value = 1f - (Vector3.Distance(transform.position, ship1.transform.position) - 250) / dist1;
        slider2.value = 1f - (Vector3.Distance(transform.position, ship2.transform.position) - 250) / dist2;
    }

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ship1") {
			SceneManager.LoadScene("Gameover1");
		}
		else if (col.gameObject.tag == "Ship2") {
			SceneManager.LoadScene("Gameover2");
		}
	}
}
