using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float speed;
	public float radius;
	Vector3 startPos;
	public Vector3 target;

	int randDirX;
	int randDirY;
	int randDirZ;

	void Awake() {
		startPos = transform.position;

		updateTarget();
	}

	void updateTarget() {
		target = startPos + (Random.insideUnitSphere * radius);
	}

	void FixedUpdate() {
		Move();
	}

	void Move() {
		if ((transform.position - target).magnitude < 1) {
			updateTarget();
		}

		transform.position = Vector3.Lerp(transform.position, target, .05f);
	}

	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Bullet1" || col.gameObject.tag == "Bullet2") {
			transform.localScale -= new Vector3(1, 1, 1);
			GetComponent<Rigidbody>().mass -= .1f;

			if (transform.localScale == Vector3.zero) {
				Destroy(gameObject);
			}
		}
	}
}
