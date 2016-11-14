using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Bullet1" || col.gameObject.tag == "Bullet2") {
			transform.localScale -= new Vector3(1, 1, 1);
		}
	}
}
