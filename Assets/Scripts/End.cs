using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class End : MonoBehaviour {

	public GameObject endText1;
	public GameObject endText2;
	public bool ended = false;

	void Update() {
		if (!ended) {
			endText1.GetComponent<Renderer>().enabled = false;
			endText2.GetComponent<Renderer>().enabled = false;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Ship1") {
			ended = true;
			endText1.GetComponent<Renderer>().enabled = true;
		}
		else if (col.gameObject.tag == "Ship2") {
			ended = true;
			endText2.GetComponent<Renderer>().enabled = true;
		}
	}
}
