﻿using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float speed;
	public float minX;
	public float maxX;

	public bool flip = false;

	void FixedUpdate () {
		Move();
	}

	void Move() {
		if (transform.position.x < minX || transform.position.x > maxX) {
			speed *= -1;
		}

		if (flip) {
			GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 0, -30) * Vector3.right * speed;
		}
		else {
			GetComponent<Rigidbody>().velocity = Quaternion.Euler(0, 0, 30) * Vector3.right * speed;
		}
	}

	void OnCollisionEnter(Collision col) {
		if (col.gameObject.tag == "Bullet1" || col.gameObject.tag == "Bullet2") {
			transform.localScale -= new Vector3(1, 1, 1);
			GetComponent<Rigidbody>().mass -= .1f;

			if (GetComponent<Collider>().bounds.size == Vector3.zero) {
				Destroy(gameObject);
			}
		}
	}
}
