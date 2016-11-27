using UnityEngine;
using System.Collections;

public class RotateToFace : MonoBehaviour {

    public GameObject face;

	void Update () {
        var targetPosition = face.transform.position;

        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
	}
}
