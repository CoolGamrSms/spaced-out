using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public float speed;
	public float radius;
	Vector3 startPos;

	int randDirX;
	int randDirY;
	int randDirZ;

	void Awake() {
		startPos = transform.position;
		randDirX = Random.Range(0, 180);
		randDirY = Random.Range(0, 180);
		randDirZ = Random.Range(0, 180);
	}

	void FixedUpdate() {
		Move();
	}

	void Move() {
		if (transform.position.x < startPos.x - radius || transform.position.x > startPos.x + radius) {
			randDirX = Random.Range(0, 180);
			speed *= -1;
		}
		else if (transform.position.y < startPos.y - radius || transform.position.y > startPos.y + radius) {
			randDirY = Random.Range(0, 180);
			speed *= -1;
		}
		else if (transform.position.z < startPos.z - radius || transform.position.z > startPos.z + radius) {
			randDirZ = Random.Range(0, 180);
			speed *= -1;
		}

		GetComponent<Rigidbody>().velocity = Quaternion.Euler(randDirX, randDirY, randDirZ) * Vector3.right * speed;
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
